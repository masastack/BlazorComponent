using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BRadio<TValue> : IRadio<TValue>
    {
        [CascadingParameter]
        public IRadioGroup<TValue>? RadioGroup { get; set; }

        [Parameter]
        public string? OnIcon { get; set; }

        [Parameter]
        public string? OffIcon { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public RenderFragment? LabelContent { get; set; }

        [Parameter, EditorRequired]
        public TValue? Value { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter]
        public EventCallback<KeyboardEventArgs> OnKeyDown { get; set; }

        [Parameter]
        public string? Label { get; set; }

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

        protected bool IsActive { get; private set; }

        protected bool IsDark
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

        private bool IsDisabled => Disabled || RadioGroup?.IsDisabled is true;

        private bool IsReadonly => Readonly || RadioGroup?.IsReadonly is true;

        private async Task HandleClick(MouseEventArgs args)
        {
            if (IsDisabled || IsReadonly || IsActive)
            {
                return;
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(new ChangeEventArgs { Value = Value });
            }

            RadioGroup?.Toggle(Value);
        }

        public void RefreshState()
        {
            if (RadioGroup is null) return;

            IsActive = EqualityComparer<TValue>.Default.Equals(RadioGroup.Value, Value);
            StateHasChanged();
        }
    }
}
