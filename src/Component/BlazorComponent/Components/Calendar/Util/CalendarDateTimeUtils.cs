using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class CalendarDateTimeUtils
    {
        private static readonly int[] _dayOfYear = new int[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };

        public static DateTime CreateUTCDate(int year, int month = 1, int day = 1)
        {
            year = year == 0 ? 1 : year;
            return new DateTime(year, month, day);
        }

        public static int FirstWeekOffset(int year, int firstDayOfWeek, int firstDayOfYear)
        {
            var firstWeekDayInFirstWeek = 7 + firstDayOfWeek - firstDayOfYear;
            var firstWeekDayOfYear = (7 + CreateUTCDate(year, 1, firstWeekDayInFirstWeek).Day - firstDayOfWeek) % 7;

            return -firstWeekDayOfYear + firstWeekDayInFirstWeek - 1;
        }

        public static int DayOfYear(int year, int month, int day, int firstDayOfWeek)
        {
            var dayOfYear = _dayOfYear[month];
            if (month > 1 && IsLeapYear(year))
                dayOfYear++;

            return dayOfYear + day;
        }

        public static int WeeksInYear(int year, int firstDayOfWeek, int firstDayOfYear)
        { 
            var weekOffset = FirstWeekOffset(year, firstDayOfWeek, firstDayOfYear);
            var weekOffsetNext = FirstWeekOffset(year + 1, firstDayOfWeek, firstDayOfYear);
            var daysInYear = IsLeapYear(year) ? 366 : 365;

            return (daysInYear - weekOffset + weekOffsetNext) / 7;
        }

        public static int WeekNumber(int year, int month, int day, int firstDayOfWeek, int localeFirstDayOfYear)
        {
            var weekOffset = FirstWeekOffset(year, firstDayOfWeek, localeFirstDayOfYear);
            var week = Math.Ceiling(((double)DayOfYear(year, month, day, firstDayOfWeek) - weekOffset) / 7);

            return (int)(week < 1 ?
                week + WeeksInYear(year - 1, firstDayOfWeek, localeFirstDayOfYear) :
                (week > WeeksInYear(year, firstDayOfWeek, localeFirstDayOfYear) ?
                week - WeeksInYear(year, firstDayOfWeek, localeFirstDayOfYear) :
                week));
        }

        public static bool IsLeapYear(this int year) =>
            ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0));
    }
}
