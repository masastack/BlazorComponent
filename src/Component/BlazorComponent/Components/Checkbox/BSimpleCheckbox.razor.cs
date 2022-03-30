using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSimpleCheckbox : IThemeable
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public EventCallback<bool> OnInput { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public string IndeterminateIcon { get; set; } = "mdi-minus-box";

        [Parameter]
        public string OnIcon { get; set; } = "mdi-checkbox-marked";

        [Parameter]
        public string OffIcon { get; set; } = "mdi-checkbox-blank-outline";

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

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

        public string ComputedIcon
        {
            get
            {
                if (Indeterminate)
                {
                    return IndeterminateIcon;
                }

                if (Value)
                {
                    return OnIcon;
                }

                return OffIcon;
            }
        }

        [Parameter]
        public bool Ripple { get; set; } = true;

        public virtual async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (OnInput.HasDelegate && !Disabled)
            {
                await OnInput.InvokeAsync(!Value);
            }
        }
    }
}
