using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableGroupedRows<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public IEnumerable<IGrouping<string, TItem>> GroupedItems => Component.GroupedItems;

        public RenderFragment GroupContent => Component.GroupContent;

        public Dictionary<string, bool> OpenCache => Component.OpenCache;
    }
}
