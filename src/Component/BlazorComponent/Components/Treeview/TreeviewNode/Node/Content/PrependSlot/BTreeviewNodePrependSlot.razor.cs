namespace BlazorComponent
{
    public partial class BTreeviewNodePrependSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>>? PrependContent => Component.PrependContent;

        public RenderFragment? ComputedPrependContent => PrependContent?.Invoke(new TreeviewItem<TItem>(Component.Item, Component.IsLeaf,
            Component.IsSelected, Component.IsIndeterminate, Component.IsActive, Component.IsOpen));
    }
}
