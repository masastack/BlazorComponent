using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSheet : BDomComponentBase, IThemeable
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [ApiDefaultValue("div")]
        public virtual string Tag { get; set; } = "div";

        [Parameter]
        public bool? Show { get; set; }

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