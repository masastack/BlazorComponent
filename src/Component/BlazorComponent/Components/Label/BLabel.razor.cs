using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BLabel : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string For { get; set; }

        [Parameter]
        public bool Required { get; set; }

        [Parameter]
        public string Tag { get; set; } = "label";

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
    }
}