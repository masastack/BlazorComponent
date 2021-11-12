using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyDayLabel<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        public RenderFragment<CalendarTimestamp> DayLabelContent => Component.DayLabelContent;

        public string DayFormatter => Component.DayFormatter(Day, false);

        public string MonthFormatter => Component.MonthFormatter(Day, Component.ShortMonths);

        public string ButtonName => Day.Day == 1 && Component.ShowMonthOnFirst ?
            $"{MonthFormatter} {DayFormatter}": DayFormatter;
    }
}
