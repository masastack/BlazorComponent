using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ICalendarDaily : IHasProviderComponent
    {
        RenderFragment IntervalHeaderContent { get; }

        RenderFragment<CalendarTimestamp> DayLabelHeaderContent { get; }

        RenderFragment<CalendarDaySlotScope> DayHeaderContent { get; }

        RenderFragment<CalendarDayBodySlotScope> IntervalContent { get; }

        List<CalendarTimestamp> Days => default;

        bool ShortWeekdays => default;

        string WeekdayFormatter(CalendarTimestamp tms, bool @short) => default;

        string DayFormatter(CalendarTimestamp tms, bool @short) => default;

        string GenIntervalLabel(CalendarTimestamp interval) => default;

        List<List<CalendarTimestamp>> Intervals() => default;

        List<CalendarTimestamp> GenDayIntervals(int index) => default;

        Func<int, int> MinutesToPixels => default;

        Func<OneOf<StringNumber, CalendarTimestamp>, bool, double> TimeToY => default;

        Func<OneOf<StringNumber, CalendarTimestamp>, OneOf<double, bool>> TimeDelta => default;

        RenderFragment<CalendarDayBodySlotScope> DayBodyContent { get; }

        ElementReference ScrollAreaElement { set; }

        ElementReference PaneElement { set; }
    }
}
