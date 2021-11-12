using System.Collections.Generic;

namespace BlazorComponent
{
    public class CalendarStackGroup
    {
        public int Start { get; set; }

        public int End { get; set; }

        public List<CalendarEventVisual> Visuals { get; set; }
    }
}
