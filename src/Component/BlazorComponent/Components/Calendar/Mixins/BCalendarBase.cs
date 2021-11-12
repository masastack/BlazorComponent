using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComponent
{
    public partial class BCalendarBase : BDomComponentBase
    {
        #region Base

        [Parameter]
        public StringNumberDate Start { get; set; } = DateTime.Now;

        [Parameter]
        public StringNumberDate End { get; set; }

        [Parameter]
        public OneOf<string, List<int>> WeekDays { get; set; } = new List<int> { 0, 1, 2, 3, 4, 5, 6 };

        [Parameter]
        public bool HideHeader { get; set; }

        [Parameter]
        public bool ShortWeekdays { get; set; } = true;

        [Parameter]
        public Func<CalendarTimestamp, bool, string> WeekdayFormat { get; set; }

        [Parameter]
        public Func<CalendarTimestamp, bool, string> DayFormat { get; set; }

        [Parameter]
        public string Locale { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumberDate Now { get; set; }

        [Parameter]
        public EventCallback<StringNumberDate> NowChanged { get; set; }

        #endregion

        public string CurrentLocale => Locale; //TODO $vuetify.lang.current

        public CalendarTimestamp Today => CalendarTimestampUtils.ParseTimestamp(Now);

        public CalendarTimestamp TimesNow => CalendarTimestampUtils.ParseTimestamp(Now);

        public List<int> ParsedWeekdays() =>
            WeekDays.Match(
                t0 => !string.IsNullOrWhiteSpace(t0) ? t0.Split(',').Select(x => int.Parse(x)).ToList() : null,
                t1 => t1);

        public List<int> WeekdaySkips() => CalendarTimestampUtils.GetWeekdaySkips(ParsedWeekdays());

        public List<int> WeekdaySkipsReverse()
        {
            var reversed = CalendarTimestampUtils.DeepCopy(WeekdaySkips());
            reversed.Reverse();

            return reversed;
        }

        public virtual CalendarTimestamp ParsedStart() => CalendarTimestampUtils.ParseTimestamp(Start);

        public virtual CalendarTimestamp ParsedEnd()
        {
            var start = ParsedStart();
            var end = End != null ? CalendarTimestampUtils.ParseTimestamp(End) : start;

            return CalendarTimestampUtils.GetTimestampIdentifier(end) < CalendarTimestampUtils.GetTimestampIdentifier(start) ? start : end;
        }

        public virtual List<CalendarTimestamp> Days =>
            CalendarTimestampUtils.CreateDayList(ParsedStart(), ParsedEnd(), Today, WeekdaySkips());

        public string DayFormatter(CalendarTimestamp tms, bool @short)
        {
            if (DayFormat != null)
                return DayFormat(tms, @short);

            var options = new CalendarFormatterOptions { TimeZone = "UTC", Day = "numeric" };

            var nativeLocaleFormatter = CalendarTimestampUtils.CreateNativeLocaleFormatter(CurrentLocale, options);

            return nativeLocaleFormatter(tms, @short);
        }

        public string WeekdayFormatter(CalendarTimestamp tms, bool @short)
        {
            if (WeekdayFormat != null)
                return WeekdayFormat(tms, @short);

            var longOptions = new CalendarFormatterOptions { TimeZone = "UTC", Weekday = "long" };
            var shortOptions = new CalendarFormatterOptions { TimeZone = "UTC", Weekday = "short" };

            var nativeLocaleFormatter = CalendarTimestampUtils.CreateNativeLocaleFormatter(
                CurrentLocale, @short ? shortOptions : longOptions);

            return nativeLocaleFormatter(tms, @short);
        }

        public CalendarTimestamp GetStartOfWeek(CalendarTimestamp timestamp) =>
            CalendarTimestampUtils.GetStartOfWeek(timestamp, ParsedWeekdays(), Today);

        public CalendarTimestamp GetEndOfWeek(CalendarTimestamp timestamp) =>
            CalendarTimestampUtils.GetEndOfWeek(timestamp, ParsedWeekdays(), Today);

        public string GetFormatter(CalendarFormatterOptions options, CalendarTimestamp tms, bool @short)
        {
            var nativeLocaleFormatter = CalendarTimestampUtils.CreateNativeLocaleFormatter(CurrentLocale, options);

            return nativeLocaleFormatter(tms, @short);
        }
    }
}
