using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class EventSlotScope
    {
        public Dictionary<string, object> Event { get; set; }

        public CalendarEventParsed EventParsed { get; set; }

        public CalendarDaySlotScope Day { get; set; }

        public bool Outside { get; set; }

        public bool Start { get; set; }

        public bool End { get; set; }

        public bool Timed { get; set; }

        public bool SingleLine { get; set; }

        public bool OverlapsNoon { get; set; }

        public Func<CalendarTimestamp, bool, string> FormatTime { get; set; }

        public Func<string> TimeSummary { get; set; }

        public Func<string> EventSummary { get; set; }
    }
}
