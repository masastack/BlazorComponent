namespace BlazorComponent
{
    public partial class BSelectSlotSelection<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        [Parameter]
        public bool Last { get; set; }

        private bool Disabled => Component.GetDisabled(Item);

        protected RenderFragment<SelectSelectionProps<TItem>> SelectionContent => Component.SelectionContent;
    }
}
