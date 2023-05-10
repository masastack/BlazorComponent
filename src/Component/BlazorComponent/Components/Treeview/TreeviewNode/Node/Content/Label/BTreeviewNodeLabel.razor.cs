namespace BlazorComponent
{
    public partial class BTreeviewNodeLabel<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>>? LabelContent => Component.LabelContent;

        public string? Text => Component.Text;

        public RenderFragment? ComputedLabelContent => LabelContent?.Invoke(new TreeviewItem<TItem>(Component.Item, Component.IsLeaf,
            Component.IsSelected, Component.IsIndeterminate, Component.IsActive, Component.IsOpen));
    }
}
