using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyWeek<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        [Parameter]
        public List<CalendarTimestamp> Week { get; set; }

        [Parameter]
        public int WeekNumber { get; set; }

        public bool ShowWeek => Component.ShowWeek;
    }
}
