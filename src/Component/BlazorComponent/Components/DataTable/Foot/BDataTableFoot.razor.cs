using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableFoot<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment FootContent=>Component.FootContent;
    }
}
