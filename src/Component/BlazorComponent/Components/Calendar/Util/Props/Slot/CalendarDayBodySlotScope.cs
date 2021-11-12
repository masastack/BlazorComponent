using OneOf;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public class CalendarDayBodySlotScope : CalendarDaySlotScope
    {
        public CalendarDayBodySlotScope(bool outside, int index, List<CalendarTimestamp> week, CalendarTimestamp day,
            Func<OneOf<StringNumber, CalendarTimestamp>, bool, double> timeToY = null,
            Func<OneOf<StringNumber, CalendarTimestamp>, OneOf<double, bool>> timeDelta = null,
            Func<int, int> minutesToPixels = null) : base(outside, index, week, day)
        {
            Week = week;
            TimeToY = timeToY;
            TimeDelta = timeDelta;
            MinutesToPixels = minutesToPixels;
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
            Outside = outside;
            Index = index;
        }

        public Func<OneOf<StringNumber, CalendarTimestamp>, bool, double> TimeToY { get; }

        public Func<OneOf<StringNumber, CalendarTimestamp>, OneOf<double, bool>> TimeDelta { get; }

        public Func<int, int> MinutesToPixels { get; }
    }
}
