using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDays<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        public List<CalendarTimestamp> Day(CalendarTimestamp timestamp)
        {
            var resultDayList = new List<CalendarTimestamp>();
            for (int i = 0; i < Component.ParsedCategories().Count; i++)
                resultDayList.Add(timestamp);

            return resultDayList.Any() ? resultDayList : new List<CalendarTimestamp> { timestamp };
        }
    }
}
