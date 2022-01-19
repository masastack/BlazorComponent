using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PageTabContentContext
    {
        public PageTabContentContext(PageTabItem item, Dictionary<string, object> attrs, Action close, bool isActive)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Attrs = attrs ?? throw new ArgumentNullException(nameof(attrs));
            Close = close ?? throw new ArgumentNullException(nameof(close));
            IsActive = isActive;
        }

        public PageTabItem Item { get; }

        public Dictionary<string, object> Attrs { get; }

        public Action Close { get; }

        public bool IsActive { get; }
    }
}
