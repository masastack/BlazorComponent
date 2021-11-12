using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyDayBody<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        public RenderFragment<CalendarDayBodySlotScope> DayBodyContent => Component.DayBodyContent;

        public CalendarDayBodySlotScope GetSlotScope =>
            new(false, Index, Component.Days, Day,Component.TimeToY, Component.TimeDelta, Component.MinutesToPixels);
    }
}
