using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    /// <summary>
    /// The group contains <see cref="BBreadcrumbsItem"/> and <see cref="BBreadcrumbsDivider"/>.
    /// </summary>
    /// <typeparam name="TBreadcrumbs">The implementation type.</typeparam>
    public partial class BBreadcrumbsItemGroup<TBreadcrumbs> : ComponentPartBase<TBreadcrumbs>
        where TBreadcrumbs : IBreadcrumbs, IBreadcrumbsDivider
    {
        [Parameter]
        public BreadcrumbItem Item { get; set; }

        protected RenderFragment<BreadcrumbItem> ItemContent => Component.ItemContent;

        protected IReadOnlyList<BreadcrumbItem> Items => Component.Items;

        protected string Divider => Component.Divider;

        protected RenderFragment DividerContent => Component.DividerContent;

        protected bool RenderDivider => Component.RenderDivider;

        protected bool IsLast => Items.Last() == Item;
    }
}