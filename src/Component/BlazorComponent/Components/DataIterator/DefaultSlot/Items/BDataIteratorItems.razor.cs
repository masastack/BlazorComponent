namespace BlazorComponent
{
    public partial class BDataIteratorItems<TItem, TComponent> where TComponent : IDataIterator<TItem>
    {
        public bool IsEmpty => Component.IsEmpty;

        public RenderFragment? ComponentChildContent => Component.ChildContent;

        public RenderFragment<ItemProps<TItem>> ItemContent => Component.ItemContent;

        public IEnumerable<TItem> ComputedItems => Component.ComputedItems;
    }
}
