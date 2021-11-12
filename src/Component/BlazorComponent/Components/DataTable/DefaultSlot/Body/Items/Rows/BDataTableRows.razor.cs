using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableRows<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public IEnumerable<TItem> Items { get; set; }

        public RenderFragment<ItemProps<TItem>> ItemContent => Component.ItemContent;
    }
}
