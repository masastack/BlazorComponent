namespace BlazorComponent
{
    public class DataTableHeader<TItem> : DataTableHeader
    {
        private ItemValue<TItem> _itemValue;

        public DataTableHeader()
        {
        }

        public DataTableHeader(string text, string value)
            : base(text, value)
        {
        }

        public DataTableHeader(string text, Func<TItem, object> itemValueFactory)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            _itemValue = new ItemValue<TItem>(itemValueFactory);
        }

        public ItemValue<TItem> ItemValue
        {
            get
            {
                if (_itemValue == null)
                {
                    _itemValue = new ItemValue<TItem>(Value);
                }

                return _itemValue;
            }
        }

        public Func<object, string, TItem, bool> Filter { get; set; }

        public bool Filterable { get; set; } = true;
    }
}
