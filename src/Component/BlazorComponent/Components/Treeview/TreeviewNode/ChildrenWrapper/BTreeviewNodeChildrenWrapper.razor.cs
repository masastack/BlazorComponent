namespace BlazorComponent
{
    public partial class BTreeviewNodeChildrenWrapper<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public bool IsOpen => Component.IsOpen;

        public List<TItem> ComputedChildren => Component.ComputedChildren;

        public bool Disabled => Component.Disabled;
    }
}
