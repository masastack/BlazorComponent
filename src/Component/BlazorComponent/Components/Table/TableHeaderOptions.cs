using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class TableHeaderOptions
    {
        public TableHeaderOptions()
        {

        }

        public TableHeaderOptions(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        public StringNumber Width { get; set; }

        public static implicit operator TableHeaderOptions(string _) => new(_);
    }
}
