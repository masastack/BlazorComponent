using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableLoading<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public Dictionary<string, object> ColspanAttrs => Component.ColspanAttrs;
    }
}
