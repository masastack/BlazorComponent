using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class EventScopeInput
    {
        public CalendarEventParsed EventParsed { get; set; }

        public CalendarDaySlotScope Day { get; set; }

        public bool Start { get; set; }

        public bool End { get; set; }

        public bool Timed { get; set; }
    }
}
