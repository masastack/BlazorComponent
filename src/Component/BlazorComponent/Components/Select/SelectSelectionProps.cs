using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class SelectSelectionProps<TItem>
    {
        public SelectSelectionProps(TItem item,int index)
        {
            Item = item;
            Index = index;
        }

        public TItem Item { get; }

        public int Index { get; }
    }
}
