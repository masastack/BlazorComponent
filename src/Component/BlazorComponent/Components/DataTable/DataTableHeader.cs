using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Value { get; set; }

        public string Text { get; set; }

        public bool Sortable { get; set; } = true;

        public string Align { get; set; }

        public bool Groupable { get; set; } = true;

        public string CellClass { get; set; }

        public StringNumber Width { get; set; }
    }
}