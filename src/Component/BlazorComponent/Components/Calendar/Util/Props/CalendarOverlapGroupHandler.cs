using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public class CalendarOverlapGroupHandler
    {
        public List<CalendarColumnGroup> Groups { get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public Action Reset { get; set; }

        public Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>> GetVisuals { get; set; }
    }
}
