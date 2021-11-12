using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyWeekNumber<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        [Parameter]
        public int WeekNumber { get; set; }
    }
}
