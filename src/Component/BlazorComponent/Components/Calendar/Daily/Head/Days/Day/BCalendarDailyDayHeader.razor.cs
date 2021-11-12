using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyDayHeader<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        public RenderFragment<CalendarDaySlotScope> DayHeaderContent => Component.DayHeaderContent;

        public CalendarDaySlotScope DayHeaderContentFormat => new(false, Index, Component.Days, Day);
    }
}
