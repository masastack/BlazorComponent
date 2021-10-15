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
