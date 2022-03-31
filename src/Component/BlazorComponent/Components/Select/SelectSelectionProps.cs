namespace BlazorComponent
{
    public class SelectSelectionProps<TItem>
    {
        public SelectSelectionProps(TItem item, int index)
        {
            Item = item;
            Index = index;
        }

        public TItem Item { get; }

        public int Index { get; }
    }
}
