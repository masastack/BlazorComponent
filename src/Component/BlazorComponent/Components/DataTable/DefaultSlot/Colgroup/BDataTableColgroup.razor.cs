using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableColgroup<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public IEnumerable<DataTableHeader<TItem>> ComputedHeaders=>Component.ComputedHeaders;
    }
}
