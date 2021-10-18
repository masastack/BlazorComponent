using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BlazorComponent
{
    public partial class BDataTableDefaultSlot<TItem,TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment TopContent => Component.TopContent;

        public bool HasTop=>Component.HasTop;

        public bool HasBottom=>Component.HasBottom;
    }
}
