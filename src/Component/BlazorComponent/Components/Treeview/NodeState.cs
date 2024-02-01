namespace BlazorComponent
{
    public class NodeState<TItem, TKey>(TItem item, IEnumerable<TKey>? children, TKey? parent = default)
    {
        public TItem Item { get; } = item;

        public IEnumerable<TKey>? Children { get; } = children;

        public TKey? Parent { get; } = parent;

        public ITreeviewNode<TItem, TKey>? Node { get; set; }

        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public bool IsIndeterminate { get; set; }

        public bool IsOpen { get; set; }
    }
}
