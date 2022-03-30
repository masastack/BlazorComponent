using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableEmptyWrapper<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public Dictionary<string, object> ColspanAttrs => Component.ColspanAttrs;
    }
}
