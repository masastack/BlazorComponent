using OneOf;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public class CategoryContentProps : CalendarTimestamp
    {
        public CategoryContentProps(List<CalendarTimestamp> week, CalendarTimestamp day,
            string category = null)
        {
            Week = week;
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
            Category = category;
        }

        public List<CalendarTimestamp> Week { get; set; }
    }
}
