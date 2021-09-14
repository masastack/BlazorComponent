using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBreadcrumbsLinkItem<TBreadcrumbsItem> : ComponentAbstractBase<TBreadcrumbsItem>
        where TBreadcrumbsItem : IBreadcrumbsItem
    {
        protected string Href => Component.Href;

        protected string Target => Component.Target;

        protected RenderFragment ChildContent => Component.ChildContent;
    }
}