using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ICalendarWeekly : IHasProviderComponent
    {
        bool ShowWeek => default;

        List<CalendarTimestamp> TodayWeek() => default;

        bool ShortWeekdays => default;

        string WeekdayFormatter(CalendarTimestamp tms, bool @short) => default;

        int GetWeekNumber(CalendarTimestamp determineDay) => default;

        List<CalendarTimestamp> Day => default;

        int WeekDays => default;

        RenderFragment<CalendarTimestamp> DayLabelContent { get; }

        bool ShowMonthOnFirst => default;

        string DayFormatter(CalendarTimestamp tms, bool @short) => default;

        string MonthFormatter(CalendarTimestamp tms, bool @short) => default;

        bool ShortMonths => default;

        RenderFragment<CalendarDaySlotScope> DayContent { get;}

        bool IsOutside(CalendarTimestamp day) => default;
    }
}
