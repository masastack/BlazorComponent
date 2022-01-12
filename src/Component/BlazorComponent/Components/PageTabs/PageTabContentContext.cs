using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PageTabContentContext
    {
        public PageTabContentContext(PageTabItem item, Dictionary<string, object> attrs)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Attrs = attrs ?? throw new ArgumentNullException(nameof(attrs));
        }

        public PageTabItem Item { get; }

        public Dictionary<string, object> Attrs { get; }

        public void Open()
        {
            PageTabItemManager.Open(Item);
        }

        public void Close()
        {
            PageTabItemManager.Close(Item);
        }

    }
}
