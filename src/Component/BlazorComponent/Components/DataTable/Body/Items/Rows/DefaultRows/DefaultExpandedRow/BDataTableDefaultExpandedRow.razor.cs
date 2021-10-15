using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableDefaultExpandedRow<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public TItem Item { get; set; }

        public bool IsExpanded => Component.IsExpanded(Item);

        public RenderFragment<(List<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent => Component.ExpandedItemContent;

        public (List<DataTableHeader<TItem>> Headers, TItem Item) Props => (Component.ComputedHeaders, Item);
    }
}
