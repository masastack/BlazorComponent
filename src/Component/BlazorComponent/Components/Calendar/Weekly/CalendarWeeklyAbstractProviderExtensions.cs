namespace BlazorComponent
{
    public static class CalendarWeeklyAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyCalendarWeeklyDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BCalendarWeeklyDay<>), typeof(BCalendarWeeklyDay<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyDayLabel<>), typeof(BCalendarWeeklyDayLabel<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyHead<>), typeof(BCalendarWeeklyHead<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyHeadDay<>), typeof(BCalendarWeeklyHeadDay<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyHeadDays<>), typeof(BCalendarWeeklyHeadDays<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyWeek<>), typeof(BCalendarWeeklyWeek<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyWeekNumber<>), typeof(BCalendarWeeklyWeekNumber<ICalendarWeekly>))
                .Apply(typeof(BCalendarWeeklyWeeks<>), typeof(BCalendarWeeklyWeeks<ICalendarWeekly>));
        }
    }
}
