using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableHeaders<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment HeaderContent => Component.HeaderContent;

        public bool HideDefaultHeader => Component.HideDefaultHeader;

        public StringBoolean Loading => Component.Loading;
    }
}
