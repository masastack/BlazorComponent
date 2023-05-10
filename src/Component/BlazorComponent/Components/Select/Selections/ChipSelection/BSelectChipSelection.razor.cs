namespace BlazorComponent
{
    public partial class BSelectChipSelection<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        [Parameter, EditorRequired]
        public TItem Item { get; set; } = default!;

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public bool Selected { get; set; }

        [Parameter]
        public bool Last { get; set; }

        protected string? GetText(TItem item) => Component.GetText(item);
    }
}
