namespace BlazorComponent
{
    public class NodeState<TItem, TKey>
    {
        public TKey Parent { get; set; }

        public IEnumerable<TKey> Children { get; set; }

        public ITreeviewNode<TItem, TKey> Node { get; set; }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public bool IsIndeterminate { get; set; }

        public bool IsOpen { get; set; }

        public TItem Item { get; set; }
    }
}
