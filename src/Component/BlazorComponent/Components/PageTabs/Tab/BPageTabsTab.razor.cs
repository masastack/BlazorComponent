using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BPageTabsTab<TTabs> : ComponentPartBase<TTabs>
        where TTabs : IPageTabs
    {
        IList<PageTabItem> ComputedItems => Component.ComputedItems;

        string CloseIcon => Component.CloseIcon;

        RenderFragment<PageTabContentContext> TabContent => Component.TabContent;

        bool IsActive(PageTabItem item) => Component.IsActive(item);

        void Close(PageTabItem item) => Component.Close(item);
    }
}
