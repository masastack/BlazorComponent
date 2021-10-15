using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ItemColProps<TItem>
    {
        public DataTableHeader<TItem> Header { get; set; }

        public object Value => Header.ItemValue.Invoke(Item);

        public TItem Item { get; set; }
    }
}
