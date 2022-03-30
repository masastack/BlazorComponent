namespace BlazorComponent
{
    public class SelectListItemProps<TItem>
    {
        public SelectListItemProps(TItem item)
        {
            Item = item;
        }

        public TItem Item { get; }
    }
}
