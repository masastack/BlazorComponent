using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyDays<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        public List<CalendarTimestamp> Days => Component.Days;
    }
}
