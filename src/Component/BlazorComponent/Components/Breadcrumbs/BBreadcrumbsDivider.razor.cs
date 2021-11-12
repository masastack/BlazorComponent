using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    /// <summary>
    /// The component that is used to separate the <see cref="BBreadcrumbsItem"/>s.
    /// </summary>
    public partial class BBreadcrumbsDivider
    {
        [Parameter]
        public virtual string Tag { get; set; } = "li";

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}