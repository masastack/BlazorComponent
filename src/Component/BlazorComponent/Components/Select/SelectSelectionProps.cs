namespace BlazorComponent
{
    public class SelectSelectionProps<TItem>
    {
        public SelectSelectionProps(TItem item, int index, bool selected)
        {
            Item = item;
            Index = index;
            Selected = selected;
        }

        public TItem Item { get; }

        public int Index { get; }
        
        public bool Selected { get;  }
    }
}
