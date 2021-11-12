using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class CalendarColumn
    {
        private const int FullWidth = 100;

        public static Func<List<CalendarEventParsed>, int, int,
            Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>>> Column =>
            (events, firstWeekday, overlapThreshold) =>
            {
                var handler = CalendarCommon.GetOverlapGroupHandler(firstWeekday);

                return (day, dayEvents, timed, reset) =>
                {
                    var visuals = handler.GetVisuals(day, dayEvents, timed, reset);

                    if (timed)
                    {
                        visuals.ForEach(visual =>
                        {
                            visual.Left = visual.Column * FullWidth / visual.ColumnCount;
                            visual.Width = FullWidth / visual.ColumnCount;
                        });
                    }

                    return visuals;
                };
            };
    }
}
