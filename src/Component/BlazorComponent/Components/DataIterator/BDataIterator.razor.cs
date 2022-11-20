namespace BlazorComponent
{
    public partial class BDataIterator<TItem>
    {
        private Dictionary<TItem, (bool visible, bool expand)> _childrenOpenCache = new();

        internal void UpdateExpand(TItem item, (bool visible, bool expand) value)
        {
            Console.WriteLine("update expand");
            _childrenOpenCache[item] = value;
        }

        internal bool TryGetExpand(TItem item, out (bool visible, bool expand) valueTuple)
        {
            if (_childrenOpenCache.TryGetValue(item, out var value))
            {
                valueTuple = value;
                return true;
            }

            valueTuple = (false, false);
            return false;
        }
    }
}
