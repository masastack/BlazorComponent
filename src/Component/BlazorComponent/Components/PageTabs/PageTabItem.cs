using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PageTabItem
    {
        public PageTabItem(string name, string url)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public string Name { get; set; }

        public string Url { get; set; }

        internal bool IsOpened { get; set; }

        internal DateTime OpenedTime { get; set; }
    }
}
