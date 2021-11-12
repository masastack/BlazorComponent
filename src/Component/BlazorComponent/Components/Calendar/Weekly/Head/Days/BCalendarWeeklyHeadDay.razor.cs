using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyHeadDay<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        public string WeekdayFormatter => Component.WeekdayFormatter(Day, Component.ShortWeekdays);
    }
}
