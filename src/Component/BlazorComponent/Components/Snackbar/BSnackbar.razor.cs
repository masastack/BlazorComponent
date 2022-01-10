using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSnackbar : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

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
                // Snackbar is dark by default
                // override themeable logic.
                if (HasBackground)
                {
                    return !Light;
                }

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

        protected bool HasBackground
        {
            get
            {
                return !Text && !Outlined;
            }
        }
    }
}
