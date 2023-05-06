using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    /// <summary>
    /// The component that is used to separate the <see cref="BBreadcrumbsItem"/>s.
    /// </summary>
    public partial class BBreadcrumbsDivider : BDomComponentBase, IBreadcrumbsDivider
    {
        protected string Tag { get; init; } = "li";

        [Parameter]
        public string Divider { get; set; } = "/";

        [Parameter]
        public RenderFragment? DividerContent { get; set; }
    }
}