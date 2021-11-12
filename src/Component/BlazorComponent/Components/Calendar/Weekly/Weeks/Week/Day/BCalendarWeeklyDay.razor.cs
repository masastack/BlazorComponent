using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyDay<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public List<CalendarTimestamp> Week { get; set; }

        public RenderFragment<CalendarDaySlotScope> DayContent => Component.DayContent;

        public CalendarDaySlotScope DayContentProps => 
            new(Component.IsOutside(Day), Index, Week, Day);
    }
}
