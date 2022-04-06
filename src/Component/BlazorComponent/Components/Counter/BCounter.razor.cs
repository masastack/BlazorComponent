using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BCounter : IThemeable
    {
        [Parameter]
        public StringNumber Value { get; set; } = "";

        [Parameter]
        public StringNumber Max { get; set; }

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
