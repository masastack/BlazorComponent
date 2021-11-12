using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarWeeklyHeadDays<TCalendarWeekly> where TCalendarWeekly : ICalendarWeekly
    {
        public List<CalendarTimestamp> TodayWeek => Component.TodayWeek();

        public bool ShowWeek => Component.ShowWeek;
    }
}
