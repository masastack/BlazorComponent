using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCalendarDailyBodyIntervals<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        public List<CalendarTimestamp> Intervals
        {
            get
            {
                var intervals = Component.Intervals();

                return intervals != null && intervals.Count > 0 ? intervals[0] : new();
            }
        }
    }
}
