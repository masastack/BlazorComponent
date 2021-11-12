using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDaily : BCalendarIntervals
    {
        [Parameter]
        public RenderFragment IntervalHeaderContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarTimestamp> DayLabelHeaderContent { get; set; }

        [Parameter]
        public virtual RenderFragment<CalendarDaySlotScope> DayHeaderContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDayBodySlotScope> IntervalContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDayBodySlotScope> DayBodyContent { get; set; }

        [Parameter]
        public RenderFragment<CategoryContentProps> CategoryContent { get; set; }
    }
}
