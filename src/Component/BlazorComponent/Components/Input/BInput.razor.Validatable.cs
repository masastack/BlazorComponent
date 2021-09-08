using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInput<TValue>
    {
        private TValue _lazyValue;
        private bool _isFocused;

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool ValidateOnBlur { get; set; }

        [Parameter]
        public virtual TValue Value
        {
            get
            {
                return _lazyValue;
            }
            set
            {
                _lazyValue = value;
            }
        }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        [CascadingParameter]
        public BForm Form { get; set; }

        [CascadingParameter]
        public EditContext EditContext { get; set; }

        protected EditContext OldEditContext { get; set; }

        protected FieldIdentifier ValueIdentifier { get; set; }

        protected bool HasInput { get; set; }

        protected bool HasFocused { get; set; }

        protected TValue InternalValue
        {
            get
            {
                return _lazyValue;
            }
            set
            {
                _lazyValue = value;
                HasInput = true;

                //REVIEW:Is this ok?
                _ = ValueChanged.InvokeAsync(_lazyValue);
                if (!ValidateOnBlur) Validate();
            }
        }

        public bool IsFocused
        {
            get
            {
                return _isFocused;
            }
            protected set
            {
                _isFocused = value;

                if (!_isFocused && !IsDisabled)
                {
                    HasFocused = true;
                    if (ValidateOnBlur) Validate();
                }
            }
        }

        public List<string> ErrorMessages { get; protected set; } = new();

        public virtual bool HasError => ErrorMessages.Count > 0;

        public virtual bool HasState
        {
            get
            {
                if (IsDisabled)
                {
                    return false;
                }

                return HasError;
            }
        }

        public virtual bool IsDisabled => Disabled || (Form != null && Form.Disabled);

        public virtual bool IsReadonly => Readonly || (Form != null && Form.Readonly);

        public virtual bool ShouldValidate
        {
            get
            {
                if (ValidateOnBlur)
                {
                    return HasFocused && !IsFocused;
                }

                return HasInput || HasFocused;
            }
        }

        protected override void OnParametersSet()
        {
            if (Prepend != null)
            {
                PrependContent = Prepend;
            }

            if (Append != null)
            {
                AppendContent = Append;
            }

            SubscribeValidationStateChanged();
        }

        protected virtual void Validate()
        {
            if (EditContext != null && ValueExpression != null)
            {
                EditContext.NotifyFieldChanged(ValueIdentifier);
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

        protected virtual void HandleOnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            ErrorMessages = EditContext.GetValidationMessages(ValueIdentifier).ToList();
            StateHasChanged();
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
