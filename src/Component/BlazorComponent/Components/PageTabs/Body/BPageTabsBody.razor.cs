using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPageTabsBody<TTabs> : ComponentPartBase<TTabs>
        where TTabs : IPageTabs
    {
        IList<PageTabItem> ComputedItems => Component.ComputedItems;

        RenderFragment ComponentChildContent => Component.ChildContent;

        bool IsActive(PageTabItem item) => Component.IsActive(item);

        bool NoActiveItem => Component.NoActiveItem;
    }
}
