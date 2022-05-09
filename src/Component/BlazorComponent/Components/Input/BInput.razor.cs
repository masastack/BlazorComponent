using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BInput<TValue> : BDomComponentBase, IInput<TValue>
    {
        [Parameter]
        public RenderFragment AppendContent { get; set; }

        [Parameter]
        public virtual string AppendIcon { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public virtual string PrependIcon { get; set; }

        [Parameter]
        public RenderFragment LabelContent { get; set; }

        [Parameter]
        public RenderFragment PrependContent { get; set; }

        [Parameter]
        public StringBoolean HideDetails { get; set; } = false;

        [Parameter]
        public string Hint { get; set; }

        [Parameter]
        public bool PersistentHint { get; set; }

        [Parameter]
        public StringBoolean Loading { get; set; } = false;

        [Parameter]
        public RenderFragment<string> MessageContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMouseDown { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnMouseUp { get; set; }

        public ElementReference InputSlotElement { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnPrependClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnAppendClick { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        private bool DebounceEnabled => DebounceMilliseconds > 0;

        private Timer _debounceTimer;

        public virtual Func<Task> DebounceTimerRun { get; set; }

        public virtual int DebounceMilliseconds { get; set; } = 250;

        public virtual bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        protected bool HasMouseDown { get; set; }

        public virtual bool HasLabel => LabelContent != null || Label != null;

        public virtual bool HasDetails => MessagesToDisplay.Count > 0;

        public bool IsLoading => Loading != null && Loading != false;

        public virtual bool ShowDetails => HideDetails == false || (HideDetails == "auto" && HasDetails);

        public virtual bool HasHint => !HasError && !string.IsNullOrEmpty(Hint) && (PersistentHint || IsFocused);

        public virtual List<string> MessagesToDisplay
        {
            get
            {
                if (HasHint)
                {
                    return new List<string>
                    {
                        Hint
                    };
                }

                if (!HasMessages)
                {
                    return new List<string>();
                }

                return ValidationTarget.Take(ErrorCount).ToList();
            }
        }

        //We want InternalValue to be protected
        TValue IInput<TValue>.InternalValue => InternalValue;

        public virtual async Task ChangeValue(bool ignoreDebounce = false)
        {
            if (DebounceEnabled)
            {
                if (DebounceTimerRun != null && !ignoreDebounce)
                {
                    DebounceChangeValue();
                    return;
                }

                if (_debounceTimer != null)
                {
                    await _debounceTimer.DisposeAsync();
                    _debounceTimer = null;
                }
            }

            DebounceTimerRun?.Invoke();
        }

        protected void DebounceChangeValue()
        {
            _debounceTimer?.Dispose();
            _debounceTimer = new Timer(DebounceTimerIntervalOnTick, null, DebounceMilliseconds, DebounceMilliseconds);
        }

        protected void DebounceTimerIntervalOnTick(object state)
        {
            InvokeAsync(async () =>
            {
                await ChangeValue(true);
            });
        }

        public virtual async Task HandleOnPrependClickAsync(MouseEventArgs args)
        {
            if (OnPrependClick.HasDelegate)
            {
                await OnPrependClick.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnAppendClickAsync(MouseEventArgs args)
        {
            if (OnAppendClick.HasDelegate)
            {
                await OnAppendClick.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnMouseDownAsync(MouseEventArgs args)
        {
            HasMouseDown = true;
            if (OnMouseDown.HasDelegate)
            {
                await OnMouseDown.InvokeAsync(args);
            }
        }

        public virtual async Task HandleOnMouseUpAsync(MouseEventArgs args)
        {
            HasMouseDown = false;
            if (OnMouseUp.HasDelegate)
            {
                await OnMouseUp.InvokeAsync(args);
            }
        }
    }
}
