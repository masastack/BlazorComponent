using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent
{
    public partial class BCalendarDailyDayIntervals<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        public List<CalendarTimestamp> Intervals => Component.GenDayIntervals(Index);

        public RenderFragment<CalendarDayBodySlotScope> IntervalContent => Component.IntervalContent;

        public CalendarDayBodySlotScope GetSlotScope(CalendarTimestamp interval) =>
            new(false, Index, Intervals, interval, Component.TimeToY, Component.TimeDelta, Component.MinutesToPixels);
    }
}
