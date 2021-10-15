using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableDefaultRows<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment<(List<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent => Component.ExpandedItemContent;

        [Parameter]
        public IEnumerable<TItem> Items { get; set; }
    }
}
