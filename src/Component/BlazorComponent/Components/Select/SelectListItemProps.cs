using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class SelectListItemProps<TItem>
    {
        public SelectListItemProps(TItem item)
        {
            Item = item;
        }

        public TItem Item { get; }
    }
}
