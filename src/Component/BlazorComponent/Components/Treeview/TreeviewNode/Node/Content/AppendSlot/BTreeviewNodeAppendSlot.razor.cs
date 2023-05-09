namespace BlazorComponent
{
    public partial class BTreeviewNodeAppendSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>>? AppendContent => Component.AppendContent;

        public RenderFragment? ComputedAppendContent => AppendContent?.Invoke(new TreeviewItem<TItem>(Component.Item, Component.IsLeaf,
            Component.IsSelected, Component.IsIndeterminate, Component.IsActive, Component.IsOpen));
    }
}
