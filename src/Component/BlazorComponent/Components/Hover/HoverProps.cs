using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class HoverProps
    {
        public HoverProps(string @class, string style, string id, bool hover)
        {
            Class = @class;
            Style = style;
            Hover = hover;
            Attrs = new Dictionary<string, object>
            {
                { "class", @class },
                { "style", style },
                { id, true }
            };
        }

        public string Class { get; }

        public string Style { get; }

        public Dictionary<string, object> Attrs { get; }

        public bool Hover { get; }
    }
}
