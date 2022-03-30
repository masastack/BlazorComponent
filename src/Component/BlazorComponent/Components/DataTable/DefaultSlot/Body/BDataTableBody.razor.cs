using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableBody<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment BodyPrependContent => Component.BodyPrependContent;

        public RenderFragment BodyAppendContent => Component.BodyAppendContent;
    }
}
