using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableBody<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment BodyPrependContent => Component.BodyPrependContent;

        public RenderFragment BodyAppendContent => Component.BodyAppendContent;
    }
}
