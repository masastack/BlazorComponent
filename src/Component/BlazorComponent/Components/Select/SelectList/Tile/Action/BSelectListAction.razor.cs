namespace BlazorComponent
{
    public partial class BSelectListAction<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public bool Value { get; set; }
    }
}
