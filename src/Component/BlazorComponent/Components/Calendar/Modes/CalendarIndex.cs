using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class CalendarIndex
    {
        public static Dictionary<string, Func<List<CalendarEventParsed>, int, int,
            Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>>>> CalendarEventOverlapModes =>
            new Dictionary<string, Func<List<CalendarEventParsed>, int, int,
                Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>>>>
                {
                    { "stack", CalendarStack.Stack },
                    { "column", CalendarColumn.Column }
                };
    }
}
