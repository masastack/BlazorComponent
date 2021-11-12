using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarWeekly : BCalendarBase
    {
        [Parameter]
        public StringNumber LocaleFirstDayOfYear { get; set; } = 0;

        [Parameter]
        public int MinWeeks { get; set; } = 1;

        [Parameter]
        public bool ShortMonths { get; set; } = true;

        [Parameter]
        public bool ShowMonthOnFirst { get; set; } = true;

        [Parameter]
        public bool ShowWeek { get; set; }

        [Parameter]
        public Func<CalendarTimestamp, bool, string> MonthFormat { get; set; }

        [Parameter]
        public RenderFragment<CalendarTimestamp> DayLabelContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDaySlotScope> DayContent { get; set; }
    }
}
