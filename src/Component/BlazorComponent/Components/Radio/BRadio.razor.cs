using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BRadio<TValue>
    {
        [Parameter]
        public string OnIcon { get; set; }

        [Parameter]
        public string OffIcon { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }
        
        [Parameter]
        public RenderFragment LabelContent { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool Ripple { get; set; } = true;

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }
        
        [CascadingParameter(Name = "Input_IsDisabled")]
        protected bool InputIsDisabled { get; set; }

        protected bool IsActive { get; set; }

        public bool IsDark
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

        public event Func<BRadio<TValue>, Task> NotifyChange;

        protected virtual async Task HandleClick(MouseEventArgs args)
        {
            if (Disabled || Readonly || IsActive)
            {
                return;
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(new ChangeEventArgs { Value = Value });
            }

            await NotifyChange?.Invoke(this);
        }

        public void Active()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            StateHasChanged();
        }

        public void DeActive()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            StateHasChanged();
        }
    }
}
