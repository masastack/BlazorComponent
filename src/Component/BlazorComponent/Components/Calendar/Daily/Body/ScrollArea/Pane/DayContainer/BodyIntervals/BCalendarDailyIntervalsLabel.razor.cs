using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyIntervalsLabel<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        [Parameter]
        public CalendarTimestamp Interval { get; set; }

        public string Label => Component.GenIntervalLabel(Interval);
    }
}
