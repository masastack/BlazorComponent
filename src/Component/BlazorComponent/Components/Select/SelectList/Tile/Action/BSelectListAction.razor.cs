namespace BlazorComponent
{
    public partial class BSelectListAction<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; } = default!;

        [Parameter]
        public bool Value { get; set; }
    }
}
