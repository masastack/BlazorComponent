namespace BlazorComponent
{
    public partial class BDataIterator<TItem>
    {
        /// <summary>
        /// cache the item status, useful for the tree data
        /// </summary>
        private readonly Dictionary<TItem, (bool visible, bool expand)> _treeItemStatusCache = new();

        internal void UpdateTreeItemStatus(TItem item, (bool visible, bool expand) value)
        {
            _treeItemStatusCache[item] = value;
        }

        internal bool TruGetTreeItemStatus(TItem item, out(bool visible, bool expand) valueTuple)
        {
            if (_treeItemStatusCache.TryGetValue(item, out var value))
            {
                valueTuple = value;
                return true;
            }

            valueTuple = (false, false);
            return false;
        }
    }
}
