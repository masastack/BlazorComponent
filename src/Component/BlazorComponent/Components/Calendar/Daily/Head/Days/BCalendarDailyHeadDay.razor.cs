using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyHeadDay<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public List<CalendarTimestamp> Days { get; set; }

        RenderFragment<CalendarDaySlotScope> DayHeaderContent => Component.DayHeaderContent;

        public CalendarDaySlotScope DayHeaderContentFormat(CalendarTimestamp day) =>
            new CalendarDaySlotScope(false, Index, Component.Days, day);
    }
}
