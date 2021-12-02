using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSheet : BDomComponentBase, IThemeable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public virtual string Tag { get; set; } = "div";

        public virtual bool IsDark { get; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }
    }
}