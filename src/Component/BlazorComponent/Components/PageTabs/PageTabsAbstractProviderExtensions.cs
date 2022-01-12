using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class PageTabsAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyPageTabsDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BTabsTab<>), typeof(BPageTabsTab<IPageTabs>))
                .Merge(typeof(BTabsBody<>), typeof(BPageTabsBody<IPageTabs>))
                .Apply(typeof(BPageTabsMenu<>), typeof(BPageTabsMenu<IPageTabs>));
        }
    }
}
