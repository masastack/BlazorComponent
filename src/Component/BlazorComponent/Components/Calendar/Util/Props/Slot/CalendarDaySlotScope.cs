using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class CalendarDaySlotScope : CalendarTimestamp
    {
        public CalendarDaySlotScope(bool outside, int index,
            List<CalendarTimestamp> week, CalendarTimestamp day)
        {
            Week = week;
            Outside = outside;
            Index = index;
            Date = day.Date;
            Time = day.Time;
            Year = day.Year;
            Month = day.Month;
            Day = day.Day;
            WeekDay = day.WeekDay;
            Hour = day.Hour;
            Minute = day.Minute;
            Past = day.Past;
            Present = day.Present;
            Future = day.Future;
        }

        public List<CalendarTimestamp> Week { get; set; }

        public bool Outside { get; set; }

        public int Index { get; set; }
    }
}
