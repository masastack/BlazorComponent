using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableScopedRows<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public IEnumerable<TItem> ComputedItems => Component.ComputedItems;

        public RenderFragment<(int Index, TItem Item)> ItemContent => Component.ItemContent;

        public Func<TItem, bool> IsExpanded => Component.IsExpanded;

        public RenderFragment<(IEnumerable<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent => Component.ExpandedItemContent;
    }
}
