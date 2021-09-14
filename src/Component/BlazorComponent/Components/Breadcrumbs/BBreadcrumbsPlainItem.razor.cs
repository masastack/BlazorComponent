using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBreadcrumbsPlainItem<TBreadcrumbsItem> : ComponentAbstractBase<TBreadcrumbsItem>
        where TBreadcrumbsItem : IBreadcrumbsItem
    {
        protected string Tag { get; init; } = "div";

        protected RenderFragment ChildContent => Component.ChildContent;
    }
}