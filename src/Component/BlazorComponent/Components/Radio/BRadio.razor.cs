using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
#if NET6_0
    public partial class BRadio<TValue> : IRadio<TValue>
#else
    public partial class BRadio<TValue> : IRadio<TValue> where TValue : notnull
#endif
    {
        [CascadingParameter]
        public IRadioGroup<TValue>? RadioGroup { get; set; }

        [Parameter]
        [MassApiParameter("$radioOn")]
        public string? OnIcon { get; set; } = "$radioOn";

        [Parameter]
        [MassApiParameter("$radioOff")]
        public string? OffIcon { get; set; } = "$radioOff";

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public RenderFragment? LabelContent { get; set; }

        [Parameter]
        public TValue Value { get; set; } = default!;

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
        [MassApiParameter(true)]
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

            if (RadioGroup is not null)
            {
                await RadioGroup.Toggle(Value);
            }
        }

        public void RefreshState()
        {
            if (RadioGroup is null) return;

            IsActive = EqualityComparer<TValue>.Default.Equals(RadioGroup.Value, Value);
            StateHasChanged();
        }
    }
}
