using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyHeadDayLabel<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        public RenderFragment<CalendarTimestamp> DayLabelHeaderContent => Component.DayLabelHeaderContent;

        public string DayFormatter => Component.DayFormatter(Day, false);
    }
}
