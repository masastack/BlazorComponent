using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorComponent
{
    public static class CalendarTimestampUtils
    {
        public const string ParseRegex = @"^(\d{4})-(\d{1,2})(-(\d{1,2}))?([^\d]+(\d{1,2}))?(:(\d{1,2}))?(:(\d{1,2}))?$";
        public const string ParseTimeRegex = @"(\d\d?)(:(\d\d?)|)(:(\d\d?)|)";

        public static int[] DaysInMonth = new int[13] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static int[] DaysInMonthLeap = new int[13] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public const int DayMax = 12;
        public const int DaysInMonthMin = 28;
        public const int DaysInMonthMax = 31;
        public const int MonthMax = 12;
        public const int MonthMin = 1;
        public const int DayMin = 1;
        public const int DaysInWeek = 7;
        public const int MinutesInHour = 60;
        public const int MinuteMax = 59;
        public const int MinutesInDay = 24 * 60;
        public const int HoutsInDay = 24;
        public const int HourMax = 23;
        public const int FirstHour = 0;
        public const int OffsetYear = 10000;
        public const int OffsetMonth = 100;
        public const int OffsetHour = 100;
        public const int OffsetTime = 10000;

        private static int _time = 0;
        private static string _timeStr = "AM";

        public static T DeepCopy<T>(T obj)
        {
            if (obj is null || obj is string || obj.GetType().IsValueType) return obj;
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopy(field.GetValue(obj))); }
                catch { }
            }

            return (T)retval;
        }

        public static CalendarTimestamp GetStartOfWeek(CalendarTimestamp timestamp,
            List<int> weekdays, CalendarTimestamp today = null)
        {
            var start = DeepCopy(timestamp);
            FindWeekday(start, weekdays[0], PrevDay);
            UpdateFormatted(start);
            if (today != null)
            { 
                UpdateRelative(start, today, start.HasTime);
            }

            return start;
        }

        public static CalendarTimestamp GetEndOfWeek(CalendarTimestamp timestamp,
            List<int> weekdays, CalendarTimestamp today = null)
        {
            var end = DeepCopy(timestamp);
            FindWeekday(end, weekdays.Last());
            UpdateFormatted(end);
            if (today != null)
            {
                UpdateRelative(end, today, end.HasTime);
            }

            return end;
        }

        public static CalendarTimestamp GetStartOfMonth(CalendarTimestamp timestamp)
        {
            var start = DeepCopy(timestamp);
            start.Day = DayMin;
            UpdateWeekday(start);
            UpdateFormatted(start);

            return start;
        }

        public static CalendarTimestamp GetendOfMonth(CalendarTimestamp timestamp)
        {
            var end = DeepCopy(timestamp);
            end.Day = DaysInMonths(end.Year, end.Month);
            UpdateWeekday(end);
            UpdateFormatted(end);

            return end;
        }

        public static CalendarTimestamp UpdateWeekday(CalendarTimestamp timestamp)
        {
            timestamp.WeekDay = GetWeekday(timestamp);

            return timestamp;
        }

        public static int GetWeekday(CalendarTimestamp timestamp)
        {
            if (timestamp.HasDay)
            {
                var k = timestamp.Day;
                var m = ((timestamp.Month + 9) % DayMax) + 1;
                var c = Math.Floor((double)timestamp.Year / 100);
                var y = (timestamp.Year % 100) - (timestamp.Month <= 2 ? 1 : 0);

                return (int)(((k + Math.Floor(2.6 * m - 0.2) - 2 * c + y +
                    Math.Floor((double)(y / 4)) + Math.Floor(c / 4)) % 7) + 7) % 7;
            }

            return timestamp.WeekDay;
        }

        public static CalendarTimestamp FindWeekday(CalendarTimestamp timestamp,
            int weekday, Func<CalendarTimestamp, CalendarTimestamp> mover = null, int maxDays = 6)
        {
            mover ??= NextDay;
            while (timestamp.WeekDay != weekday && --maxDays >= 0)
                timestamp = mover(timestamp);

            return timestamp;
        }

        public static Func<CalendarTimestamp, CalendarTimestamp> PrevDay = timestamp =>
        {
            timestamp.Day--;
            timestamp.WeekDay = (timestamp.WeekDay + 6) % DaysInWeek;
            if (timestamp.Day < DayMin)
            {
                timestamp.Month--;
                if (timestamp.Month < MonthMin)
                {
                    timestamp.Year--;
                    timestamp.Month = MonthMax;
                }
                timestamp.Day = GetDaysInMonth(timestamp.Year, timestamp.Month);
            }

            return timestamp;
        };

        public static int GetDaysInMonth(int year, int month) =>
            year.IsLeapYear() ? DaysInMonthLeap[month] : DaysInMonth[month];

        public static List<int> GetWeekdaySkips(List<int> weekdays)
        {
            var skips = new List<int> { 1, 1, 1, 1, 1, 1, 1 };
            var filled = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < weekdays.Count; i++)
            {
                filled[weekdays[i]] = 1;
            }

            for (int k = 0; k < DaysInWeek; k++)
            {
                var skip = 1;
                for (int j = 0; j < DaysInWeek; j++)
                {
                    var next = (k + j) % DaysInWeek;
                    if (filled[next] > 0)
                        break;

                    skip++;
                }

                skips[k] = filled[k] * skip;
            }

            return skips;
        }

        public static CalendarTimestamp ParseTimestamp(StringNumberDate input, bool required = false, CalendarTimestamp now = null)
        {
            if (input.IsT1 || input.IsT2)
            {
                var inputDt = input.IsT2 ? input.AsT2 :
                    DateTime.ParseExact(input.AsT1.ToString(), "yyyy-MM-dd HH:mm:ss", null);

                var dateInt = ParseDate(inputDt);
                if (now != null)
                    UpdateRelative(dateInt, now, dateInt.HasTime);

                return dateInt;
            }

            // YYYY-MM-DD hh:mm:ss
            var parts = Regex.Matches(input.AsT0, ParseRegex);
            if (parts == null || parts.Count == 0)
            {
                if (required)
                    throw new ArgumentException($"{input} is not a valid timestamp. It must be a Date, number of milliseconds since Epoch, or a string in the format of YYYY-MM-DD or YYYY-MM-DD hh:mm. Zero-padding is optional and seconds are ignored.");
                return null;
            }

            var date = Convert.ToDateTime(parts.First().Value);
            var timestamp = new CalendarTimestamp
            {
                Date = input.AsT0,
                Time = string.Empty,
                Year = date.Year,
                Month = date.Month,
                Day = date.Day,
                Hour = date.Hour,
                Minute = date.Minute,
                WeekDay = (int)date.DayOfWeek,
                HasDay = date.Day > 0,
                HasTime = date.Hour > 0 && date.Minute > 0,
                Past = false,
                Present = false,
                Future = false
            };

            UpdateWeekday(timestamp);
            UpdateFormatted(timestamp);

            if (now != null)
                UpdateRelative(timestamp, now, timestamp.HasTime);

            return timestamp;
        }

        public static int GetTimestampIdentifier(CalendarTimestamp timestamp) =>
            GetDayIdentifier(timestamp) * OffsetTime + GetTimeIdentifier(timestamp);

        public static int GetDayIdentifier(CalendarTimestamp timestamp) =>
            timestamp.Year * OffsetYear + timestamp.Month * OffsetMonth + timestamp.Day;

        public static int GetTimeIdentifier(CalendarTimestamp timestamp) =>
            timestamp.Hour * OffsetHour + timestamp.Minute;

        public static List<CalendarTimestamp> CreateDayList(CalendarTimestamp start,
            CalendarTimestamp end, CalendarTimestamp now, List<int> weekdaySkips, int max = 42, int min = 0)
        {
            var stop = GetDayIdentifier(end);
            var days = new List<CalendarTimestamp>();
            var current = DeepCopy(start);
            var currentIdentifier = 0;
            var stopped = currentIdentifier == stop;

            if (stop < GetDayIdentifier(start))
                throw new ArgumentException("End date is earlier than start date.");

            while ((!stopped || days.Count < min) && days.Count < max)
            {
                currentIdentifier = GetDayIdentifier(start);
                stopped = stopped || currentIdentifier == stop;
                if (weekdaySkips[current.WeekDay] == 0)
                {
                    current = NextDay(current);
                    continue;
                }

                var day = DeepCopy(current);
                UpdateFormatted(day);
                UpdateRelative(day, now);
                days.Add(day);
                current = RelativeDays(current, null, weekdaySkips[current.WeekDay]);
            }

            if (!days.Any())
                throw new ArgumentException("No dates found using specified start date, end date, and weekdays.");

            return days;
        }

        public static Func<CalendarTimestamp, CalendarTimestamp> NextDay => timestamp =>
        {
            timestamp.Day++;
            timestamp.WeekDay = (timestamp.WeekDay + 1) % DaysInWeek;
            if (timestamp.Day > DaysInMonthMin &&
                timestamp.Day > DaysInMonths(timestamp.Year, timestamp.Month))
            {
                timestamp.Day = DayMin;
                timestamp.Month++;
                if (timestamp.Month > MonthMax)
                {
                    timestamp.Month = MonthMin;
                    timestamp.Year++;
                }
            }

            return timestamp;
        };

        public static int DaysInMonths(int year, int month) =>
            ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0) ?
                DaysInMonthLeap[month] : DaysInMonth[month];

        public static CalendarTimestamp UpdateFormatted(CalendarTimestamp timestamp)
        {
            timestamp.Time = GetTime(timestamp);
            timestamp.Date = GetDate(timestamp);

            return timestamp;
        }

        public static string GetTime(CalendarTimestamp timestamp)
        {
            return !timestamp.HasTime ? string.Empty :
                $"{PadNumber(timestamp.Hour, 2)}:{PadNumber(timestamp.Minute, 2)}";
        }

        public static string GetDate(CalendarTimestamp timestamp)
        {
            var str = $"{PadNumber(timestamp.Year, 4)}-{PadNumber(timestamp.Month, 2)}";
            if (timestamp.HasDay)
                str += $"-{PadNumber(timestamp.Day, 2)}";

            return str;
        }

        public static string PadNumber(int x, int length)
        {
            var padded = x.ToString();
            while (padded.Length < length)
            {
                padded = "0" + padded;
            }

            return padded;
        }

        public static CalendarTimestamp RelativeDays(
            CalendarTimestamp timestamp, Func<CalendarTimestamp, CalendarTimestamp> mover = null, int days = 1)
        {
            mover ??= NextDay;
            while (--days >= 0)
                timestamp = mover(timestamp);

            return timestamp;
        }

        public static CalendarTimestamp UpdateRelative(CalendarTimestamp timestamp, CalendarTimestamp now, bool time = false)
        {
            var a = GetDayIdentifier(now);
            var b = GetDayIdentifier(timestamp);
            var present = a == b;

            if (timestamp.HasTime && time && present)
            {
                a = GetTimeIdentifier(now);
                b = GetTimeIdentifier(timestamp);
                present = a == b;
            }

            timestamp.Past = b < a;
            timestamp.Present = present;
            timestamp.Future = b > a;

            return timestamp;
        }

        public static Func<CalendarTimestamp, bool, string> CreateNativeLocaleFormatter(
            string locale, CalendarFormatterOptions options)
        {
            Func<CalendarTimestamp, bool, string> emptyFormatter = (_t, _s) => string.Empty;

            //TODO Intl.DateTimeFormat
            //return emptyFormatter;
            return (timestamp, @short) =>
            {
                if (options.Hour == "numeric")
                {
                    _time++;
                    if (_timeStr.Equals("AM"))
                    {
                        if (_time == 12)
                            _timeStr = "PM";
                        else if (_time > 12)
                            _time = 1;
                    }
                    else
                    {
                        if (_time > 12)
                        {
                            _time = 1;
                        }
                        else if (_time == 12)
                        {
                            _time = 1;
                            _timeStr = "AM";
                        }
                    }

                    return $"{_time} {_timeStr}";
                }
                if (options.Weekday == "short")
                    return Convert.ToDateTime(timestamp.Date).DayOfWeek.ToString()[..3];

                return Convert.ToDateTime(timestamp.Date).Day.ToString();
            };
        }

        public static List<CalendarTimestamp> CreateIntervalList(CalendarTimestamp timestamp,
            int first, int minutes, int count, CalendarTimestamp now = null)
        {
            var intervalList = new List<CalendarTimestamp>();
            for (int i = 0; i < count; i++)
            {
                var mins = first + (i * minutes);
                var @int = DeepCopy(timestamp);
                intervalList.Add(UpdateMinutes(@int, mins, now));
            }

            return intervalList;
        }

        public static CalendarTimestamp UpdateHasTime(CalendarTimestamp timestamp, bool hasTime, CalendarTimestamp now = null)
        {
            if (timestamp.HasTime != hasTime)
            {
                if (!hasTime)
                {
                    //timestamp.Hour = HourMax;
                    //timestamp.Minute = MinuteMax;
                    //timestamp.Time = GetTime(timestamp);
                }
                if (now != null)
                {
                    UpdateRelative(timestamp, now, timestamp.HasTime);
                }
            }

            return timestamp;
        }

        public static CalendarTimestamp UpdateMinutes(CalendarTimestamp timestamp,
            int minutes, CalendarTimestamp now = null)
        {
            timestamp.Hour = (int)Math.Floor((double)minutes / MinutesInHour);
            timestamp.Minute = minutes % MinutesInHour;
            timestamp.Time = GetTime(timestamp);

            if (now != null)
                UpdateRelative(timestamp, now, true);

            return timestamp;
        }

        public static int ParseTime(OneOf<StringNumber, CalendarTimestamp> input)
        {
            if (input.IsT0)
                return input.AsT0.Match(t0 =>
                 {
                     var regex = new Regex(ParseTimeRegex);
                     var parts = regex.Matches(t0);
                     if (parts == null && parts.Count < 2)
                         return 0;

                     return int.Parse(parts[1].Value) * 60 + int.Parse(parts[3].Value ?? "0");
                 }, t1 => t1, t2 => (int)t2);

            var res = input.AsT1;
            var minutes = (res?.Hour ?? 0) * 60 + (res?.Minute ?? 0);

            return minutes;
        }

        public static int DiffMinutes(CalendarTimestamp min, CalendarTimestamp max)
        {
            var y = (max.Year - min.Year) * 525600;
            var m = (max.Month - min.Month) * 43800;
            var d = (max.Day - min.Day) * 1440;
            var h = (max.Hour - min.Hour) * 60;
            var mi = max.Minute - min.Minute;

            return y + m + d + h + mi;
        }

        public static CalendarTimestamp ParseDate(DateTime date) =>
            UpdateFormatted(new CalendarTimestamp
            {
                Date = date.ToString("yyyy-MM-dd"),
                Time = date.ToString("HH:mm"),
                Year = date.Year,
                Month = date.Month,
                Day = date.Day,
                WeekDay = (int)date.DayOfWeek,
                Hour = date.Hour,
                Minute = date.Minute,
                HasDay = true,
                HasTime = date.Hour > 0 || date.Minute > 0,
                Past = false,
                Present = true,
                Future = false
            });

        public static DateTime TimestampToDate(CalendarTimestamp timestamp) =>
            new DateTime(timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, timestamp.Minute, 0);
    }
}
