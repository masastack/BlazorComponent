using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ItemProps<TItem>
    {
        public ItemProps(int index, TItem item)
        {
            Index = index;
            Item = item;
        }

        public int Index { get; }

        public TItem Item { get; }
    }
}
