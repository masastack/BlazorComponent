using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneOf;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDayBodyCategory<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public OneOf<string, Dictionary<string, object>> Category { get; set; }

        public RenderFragment<CalendarDayBodySlotScope> DayBodyContent => Component.DayBodyContent;

        public CalendarDayBodySlotScope GetCategoryScope => Component.GetSlotScope(Day);
    }
}
