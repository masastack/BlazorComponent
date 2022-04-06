using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataIteratorEmptyWrapper<TItem, TDataIterator>
        where TDataIterator : IDataIterator<TItem>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
