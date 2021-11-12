namespace BlazorComponent
{
    public static class CalendarDailyAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyCalendarDailyDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BCalendarDailyBodyIntervals<>), typeof(BCalendarDailyBodyIntervals<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyIntervalsLabel<>), typeof(BCalendarDailyIntervalsLabel<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyDay<>), typeof(BCalendarDailyDay<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyDayBody<>), typeof(BCalendarDailyDayBody<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyDayIntervals<>), typeof(BCalendarDailyDayIntervals<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyDays<>), typeof(BCalendarDailyDays<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyDayContainer<>), typeof(BCalendarDailyDayContainer<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyPane<>), typeof(BCalendarDailyPane<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyScrollArea<>), typeof(BCalendarDailyScrollArea<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyBody<>), typeof(BCalendarDailyBody<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyDayHeader<>), typeof(BCalendarDailyDayHeader<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyHeadDayLabel<>), typeof(BCalendarDailyHeadDayLabel<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyHeadWeekday<>), typeof(BCalendarDailyHeadWeekday<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyHeadDay<>), typeof(BCalendarDailyHeadDay<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyHeadDays<>), typeof(BCalendarDailyHeadDays<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyHeadIntervals<>), typeof(BCalendarDailyHeadIntervals<ICalendarDaily>))
                .Apply(typeof(BCalendarDailyHead<>), typeof(BCalendarDailyHead<ICalendarDaily>));
        }
    }
}
