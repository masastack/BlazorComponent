namespace BlazorComponent
{
    public partial class BSelectSelections<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        protected bool HasChips => Component.HasChips;

        protected RenderFragment<SelectSelectionProps<TItem>>? SelectionContent => Component.SelectionContent;

        protected IList<TItem> SelectedItems => Component.SelectedItems;

        protected int SelectedIndex => Component.SelectedIndex;
    }
}
