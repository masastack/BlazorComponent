using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarIntervals : BCalendarBase
    {
        #region Intervals

        [Parameter]
        public int MaxDays { get; set; } = 7;

        [Parameter]
        public bool ShortIntervals { get; set; } = true;

        [Parameter]
        public StringNumber IntervalHeight { get; set; } = 48;

        [Parameter]
        public StringNumber IntervalWidth { get; set; } = 60;

        [Parameter]
        public StringNumber IntervalMinutes { get; set; } = 60;

        [Parameter]
        public StringNumber FirstInterval { get; set; } = 0;

        [Parameter]
        public StringNumberDate FirstTime { get; set; }

        [Parameter]
        public StringNumber IntervalCount { get; set; } = 24;

        [Parameter]
        public Func<CalendarTimestamp, bool, string> IntervalFormat { get; set; }

        [Parameter]
        public Func<CalendarTimestamp, object> IntervalStyle { get; set; }

        [Parameter]
        public Func<CalendarTimestamp, bool> ShowIntervalLabel { get; set; }

        #endregion

        public int ParsedFirstInterval => FirstInterval.ToInt32();

        public int ParsedIntervalMinutes => IntervalMinutes.ToInt32();

        public int ParsedIntervalCount => IntervalCount.ToInt32();

        public int ParsedIntervalHeight => IntervalHeight.ToInt32();

        public int ParsedFirstTime => FirstTime.Match(
            t0 => {
                _ = int.TryParse(t0, out var firstInterval);
                return firstInterval;
            }, 
            t1 => t1, 
            t2 => 0);

        public int FirstMinute()
        { 
            var time = ParsedFirstInterval;

            return time >= 0 && time <= CalendarTimestampUtils.MinutesInDay ?
                time : ParsedFirstInterval * ParsedIntervalMinutes;
        }

        public int BodyHeight => ParsedIntervalCount * ParsedIntervalHeight;

        public override List<CalendarTimestamp> Days =>
            CalendarTimestampUtils.CreateDayList(ParsedStart(), ParsedEnd(), Today, WeekdaySkips(), MaxDays);

        public List<List<CalendarTimestamp>> Intervals()
        {
            var days = Days;
            var first = FirstMinute();
            var minutes = ParsedIntervalMinutes;
            var count = ParsedIntervalCount;
            var intervals = new List<List<CalendarTimestamp>>();

            for (var i = 0; i < days.Count; i++)
                intervals.Add(CalendarTimestampUtils.CreateIntervalList(days[i], first, minutes, count, TimesNow));

            return intervals;
        }

        public string IntervalFormatter(CalendarTimestamp tms, bool @short)
        {
            if (IntervalFormat != null)
                return IntervalFormat(tms, @short);

            var longOptions = new CalendarFormatterOptions { TimeZone = "UTC", Hour = "2-digit", Minute = "2-digit" };
            var shortOptions = new CalendarFormatterOptions { TimeZone = "UTC", Hour = "numeric", Minute = "2-digit" };
            var shortHourOptions = new CalendarFormatterOptions { TimeZone = "UTC", Hour = "numeric" };

            var nativeLocaleFormatter = CalendarTimestampUtils.CreateNativeLocaleFormatter("time",
                @short ? (tms.Minute == 0 ? shortHourOptions : shortOptions) : longOptions);

            return nativeLocaleFormatter(tms, @short);
        }

        public Func<CalendarTimestamp, bool> ShowIntervalLabelDefault => interval =>
        {
            var first = Intervals()[0][0];
            var isFirst = first.Hour == interval.Hour && first.Minute == interval.Minute;

            return !isFirst;
        };

        public CalendarDayBodySlotScope GetSlotScope(CalendarTimestamp timestamp) =>
            new(false, 0, Days, timestamp, TimeToY, TimeDelta, MinutesToPixels);

        public Func<int, int> MinutesToPixels => minutes =>
            minutes / ParsedIntervalMinutes * ParsedIntervalHeight;

        public virtual Func<OneOf<StringNumber, CalendarTimestamp>, bool, double> TimeToY => (time, clamp) =>
        {
            var y = TimeDelta(time);
            if (y.IsT0)
            {
                var yInt = y.AsT0;
                yInt *= BodyHeight;
                if (clamp)
                {
                    yInt = yInt < 0 ? 0 : yInt;
                    yInt = yInt > BodyHeight ? BodyHeight : yInt;
                }

                return yInt;
            }

            return 0;
        };

        public virtual Func<OneOf<StringNumber, CalendarTimestamp>, OneOf<double, bool>> TimeDelta => time =>
        {
            var minutes = CalendarTimestampUtils.ParseTime(time);
            if (minutes == 0)
                return false;

            var min = FirstMinute();
            var gap = ParsedIntervalCount * ParsedIntervalMinutes;

            return (minutes - min) / (double)gap;
        };
    }
}
