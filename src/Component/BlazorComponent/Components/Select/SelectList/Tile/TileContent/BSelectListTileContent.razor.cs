using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectListTileContent<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected string GetFilteredText(TItem item) => Component.GetFilteredText(item);
    }
}
