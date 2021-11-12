using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDayInterval<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        [Parameter]
        public CalendarTimestamp Category { get; set; }

        [Parameter]
        public CalendarTimestamp Interval { get; set; }

        public RenderFragment<CalendarDayBodySlotScope> IntervalContent => Component.IntervalContent;

        public CalendarDayBodySlotScope GetCategoryScope => Component.GetSlotScope(Interval);
    }
}
