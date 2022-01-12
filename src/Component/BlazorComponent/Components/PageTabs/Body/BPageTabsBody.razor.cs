using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
