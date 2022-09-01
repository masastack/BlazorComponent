﻿using System.Globalization;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BInput<TValue> : IValidatable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool ValidateOnBlur { get; set; }

        [Parameter]
        public virtual TValue Value
        {
            get => GetValue<TValue>();
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        [CascadingParameter]
        public BForm Form { get; set; }

        [CascadingParameter]
        public EditContext EditContext { get; set; }

        [Parameter]
        public bool Error { get; set; }

        [Parameter]
        public int ErrorCount { get; set; } = 1;

        [Parameter]
        public List<string> ErrorMessages { get; set; } = new();

        [Parameter]
        public List<string> Messages { get; set; } = new();

        [Parameter]
        public EventCallback<TValue> OnInput { get; set; }

        [Parameter]
        public bool Success { get; set; }

        [Parameter]
        public List<string> SuccessMessages { get; set; }

        [Parameter]
        public IEnumerable<Func<TValue, StringBoolean>> Rules
        {
            get { return GetValue<IEnumerable<Func<TValue, StringBoolean>>>(); }
            set { SetValue(value); }
        }

        protected EditContext OldEditContext { get; set; }

        protected FieldIdentifier ValueIdentifier { get; set; }

        protected bool HasInput { get; set; }

        protected bool HasFocused { get; set; }

        public virtual ElementReference InputElement { get; set; }

        protected virtual TValue LazyValue
        {
            get => GetValue<TValue>();
            set => SetValue(value);
        }

        protected TValue InternalValue
        {
            get => LazyValue;
            set
            {
                LazyValue = value;
                SetValue(value);
            }
        }

        public bool IsFocused
        {
            get => GetValue<bool>();
            protected set => SetValue(value);
        }

        public List<string> ErrorBucket { get; protected set; } = new();

        public virtual bool HasError => (ErrorMessages != null && ErrorMessages.Count > 0) || ErrorBucket.Count > 0 || Error;

        public virtual bool HasSuccess => (SuccessMessages != null && SuccessMessages.Count > 0) || Success;

        public virtual bool HasState
        {
            get
            {
                if (IsDisabled)
                {
                    return false;
                }

                return HasSuccess || (ShouldValidate && HasError);
            }
        }

        public virtual bool HasMessages => ValidationTarget.Count > 0;

        public List<string> ValidationTarget
        {
            get
            {
                if (ErrorMessages.Count > 0)
                {
                    return ErrorMessages;
                }

                if (SuccessMessages != null && SuccessMessages.Count > 0)
                {
                    return SuccessMessages;
                }

                if (Messages != null && Messages.Count > 0)
                {
                    return Messages;
                }

                if (ShouldValidate)
                {
                    return ErrorBucket;
                }

                return new List<string>();
            }
        }

        public virtual bool IsDisabled => Disabled || (Form != null && Form.Disabled);

        public bool IsInteractive => !IsDisabled && !IsReadonly;

        public virtual bool IsReadonly => Readonly || (Form != null && Form.Readonly);

        protected bool FirstValidate { get; set; } = true;

        public virtual bool ShouldValidate
        {
            get
            {
                if (ExternalError)
                {
                    return true;
                }

                if (ValidateOnBlur)
                {
                    return HasFocused && !IsFocused;
                }

                return HasInput || HasFocused || FirstValidate;
            }
        }

        public virtual bool ExternalError => ErrorMessages.Count > 0 || Error;

        protected virtual void OnInternalValueChange(TValue val)
        {
            // If it's the first time we're setting input,
            // mark it with hasInput
            HasInput = true;
            
            // todo: whether to need to do validate?
            if (!ValidateOnBlur)
            {
                Validate();
            }

            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(val);
            }
        }

        protected virtual void OnLazyValueChange(TValue val)
        {
        }

        protected bool ValueChangedInternal { get; set; }

        protected virtual async Task HandleOnInputImmediately(ChangeEventArgs args)
        {
            if (BindConverter.TryConvertTo<TValue>(args.Value?.ToString(), CultureInfo.InvariantCulture, out var val))
            {
                if (ValueChanged.HasDelegate)
                {
                    ValueChangedInternal = true;
                    await ValueChanged.InvokeAsync(val);
                }
            }

            if (!ValidateOnBlur)
            {
                //We removed NextTick since it doesn't trigger render
                //and validate may not be called
                Validate();
            }
        }

        protected IJSObjectReference InputJsObjectReference { get; private set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                InputJsObjectReference = await Js.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/input.js");
                await InputJsObjectReference.InvokeVoidAsync(
                    "registerInputEvents",
                    InputElement,
                    DotNetObjectReference.Create(new Invoker<ChangeEventArgs>(HandleOnInputImmediately)),
                    DotNetObjectReference.Create(new Invoker<ChangeEventArgs>(HandleOnInputAsync)),
                    250);
            }
        }

        public virtual Task HandleOnInputAsync(ChangeEventArgs args)
        {
            return Task.CompletedTask;
        }

        protected virtual bool DisableSetValueByJsInterop => false;

        protected virtual void OnValueChanged(TValue val)
        {
            LazyValue = val;

            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} Value changed: {val}");

            if (!ValueChangedInternal)
            {
                if (!DisableSetValueByJsInterop)
                {
                    _ = NextTickWhile(async () =>
                        {
                            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} setValue {val}");
                            await InputJsObjectReference.InvokeVoidAsync("setValue", InputElement, val);
                        },
                        () => InputJsObjectReference is null);
                }
            }
            else
            {
                ValueChangedInternal = false;
            }

            StateHasChanged();
        }

        protected override void OnWatcherInitialized()
        {
            Watcher
                .Watch<TValue>(nameof(Value), (val) =>
                {
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} watch value: {val}");

                    if (EqualityComparer<TValue>.Default.Equals(val, LazyValue)) return;

                    OnValueChanged(val);
                })
                .Watch<TValue>(nameof(LazyValue), OnLazyValueChange)
                .Watch<TValue>(nameof(InternalValue), OnInternalValueChange)
                .Watch<bool>(nameof(IsFocused), async val =>
                {
                    if (!val && !IsDisabled)
                    {
                        HasFocused = true;
                        if (ValidateOnBlur)
                        {
                            Validate();
                        }
                    }

                    await OnIsFocusedChange(val);
                });
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Form?.Register(this);
        }

        protected override void OnParametersSet()
        {
            SubscribeValidationStateChanged();
        }

        protected virtual Task OnIsFocusedChange(bool val)
        {
            return Task.CompletedTask;
        }

        protected virtual void Validate()
        {
            if (FirstValidate)
            {
                FirstValidate = false;
            }

            if (EditContext != null && !EqualityComparer<FieldIdentifier>.Default.Equals(ValueIdentifier, default))
            {
                EditContext.NotifyFieldChanged(ValueIdentifier);
            }

            if (Rules != null)
            {
                ErrorBucket.Clear();

                foreach (var rule in Rules)
                {
                    var result = rule(InternalValue);
                    if (result.IsT0)
                    {
                        ErrorBucket.Add(result.AsT0);
                    }
                }

                if (ErrorBucket.Count > 0)
                {
                    StateHasChanged();
                }
            }
        }

        protected virtual void SubscribeValidationStateChanged()
        {
            if (ValueExpression != null)
            {
                ValueIdentifier = FieldIdentifier.Create(ValueExpression);
            }
            else
            {
                //No ValueExpression,subscribe is unnecessary
                return;
            }

            //When EditContext update,we should re-subscribe OnValidationStateChanged
            if (OldEditContext != EditContext)
            {
                if (OldEditContext != null)
                {
                    OldEditContext.OnValidationStateChanged -= HandleOnValidationStateChanged;
                }

                EditContext.OnValidationStateChanged += HandleOnValidationStateChanged;
                OldEditContext = EditContext;
            }
        }

        public Task<bool> ValidateAsync(bool force = false, TValue? val = default)
        {
            //No rules should be valid. 
            var valid = true;

            if (force)
            {
                HasInput = true;
                HasFocused = true;
            }

            if (Rules != null)
            {
                var value = val is null ? InternalValue : val;

                ErrorBucket.Clear();

                foreach (var rule in Rules)
                {
                    var result = rule(value);
                    if (result.IsT0)
                    {
                        ErrorBucket.Add(result.AsT0);
                    }
                }

                valid = ErrorBucket.Count > 0;
            }

            return Task.FromResult(valid);
        }

        public Task<bool> ValidateAsync()
        {
            return ValidateAsync(false);
        }

        public async Task ResetAsync()
        {
            //We will change this and InternalValue
            ErrorBucket.Clear();

            LazyValue = default;

            if (InputJsObjectReference is not null)
            {
                await InputJsObjectReference.InvokeVoidAsync("setValue", InputElement, null);
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(LazyValue);
            }
        }

        public Task ResetValidationAsync()
        {
            ErrorBucket.Clear();
            return Task.CompletedTask;
        }

        protected virtual void HandleOnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            var errors = EditContext.GetValidationMessages(ValueIdentifier);
            if (!errors.Any())
            {
                if (ErrorBucket.Count == 0)
                {
                    return;
                }

                ErrorBucket.Clear();
            }
            else
            {
                ErrorBucket = errors.ToList();
            }

            InvokeStateHasChanged();
        }

        protected virtual async Task SetInternalValueAsync(TValue internalValue)
        {
            if (EqualityComparer<TValue>.Default.Equals(internalValue, InternalValue))
            {
                return;
            }

            if (!EqualityComparer<TValue>.Default.Equals(internalValue, Value) && ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(internalValue);
            }

            InternalValue = internalValue;
            HasInput = true;

            if (!ValidateOnBlur)
            {
                //We removed NextTick since it doesn't trigger render
                //and validate may not be called
                Validate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (EditContext != null)
            {
                EditContext.OnValidationStateChanged -= HandleOnValidationStateChanged;
            }

            base.Dispose(disposing);
        }
    }
}
