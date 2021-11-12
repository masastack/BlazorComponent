using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyWeeks<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        public List<CalendarTimestamp> Day => Component.Day;

        public int WeekDays => Component.WeekDays;

        public int GetWeekNumber(CalendarTimestamp determineDay) => Component.GetWeekNumber(determineDay);
    }
}
