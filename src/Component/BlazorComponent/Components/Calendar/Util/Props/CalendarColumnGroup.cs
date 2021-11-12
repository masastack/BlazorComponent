using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class CalendarColumnGroup
    {
        public int Start { get; set; }

        public int End { get; set; }

        public List<CalendarEventVisual> Visuals { get; set; }
    }
}
