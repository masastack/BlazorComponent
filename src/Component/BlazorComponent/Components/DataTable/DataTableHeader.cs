namespace BlazorComponent
{
    public class DataTableHeader
    {
        public DataTableHeader()
        {
        }

        public DataTableHeader(string text, string value, bool sortable = true)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Sortable = sortable;
        }

        public DataTableHeader(string text, string value, StringNumber width, bool sortable = true)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Width = width;
            Sortable = sortable;
        }

        public bool Divider { get; set; }

        public string? Value { get; set; }

        public string? Text { get; set; }

        public bool Sortable { get; set; } = true;

        public DataTableHeaderAlign Align { get; set; } = DataTableHeaderAlign.Start;

        public bool Groupable { get; set; } = true;
        
        // TODO: non implementation
        public string? Class { get; set; }

        public string? CellClass { get; set; }

        public StringNumber? Width { get; set; }
    }
}