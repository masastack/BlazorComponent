using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class CalendarStackNode
    {
        public CalendarStackNode Parent { get; set; }

        public bool Sibling { get; set; }

        public int Index { get; set; }

        public CalendarEventVisual Visual { get; set; }

        public int Start { get; set; }

        public int End { get; set; }

        public List<CalendarStackNode> Children { get; set; }
    }
}
