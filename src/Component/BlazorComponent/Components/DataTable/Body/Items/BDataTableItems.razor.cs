using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableItems<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public bool IsEmpty => Component.IsEmpty;

        public IEnumerable<IGrouping<string, TItem>> GroupedItems => Component.GroupedItems;

        public IEnumerable<TItem> ComputedItems => Component.ComputedItems;
    }
}
