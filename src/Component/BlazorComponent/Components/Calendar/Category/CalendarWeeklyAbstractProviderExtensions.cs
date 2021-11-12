namespace BlazorComponent
{
    public static class CalendarCategoryAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyCalendarCategoryDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BCalendarDailyDays<>), typeof(BCalendarCategoryDays<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDay<>), typeof(BCalendarCategoryDay<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDays<>), typeof(BCalendarCategoryDays<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDayBody<>), typeof(BCalendarCategoryDayBody<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDayBodyCategory<>), typeof(BCalendarCategoryDayBodyCategory<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDayInterval<>), typeof(BCalendarCategoryDayInterval<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDayIntervals<>), typeof(BCalendarCategoryDayIntervals<ICalendarCategory>))
                .Merge(typeof(BCalendarDailyDayHeader<>), typeof(BCalendarDailyDayHeader<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDayHeaderCategory<>), typeof(BCalendarCategoryDayHeaderCategory<ICalendarCategory>))
                .Apply(typeof(BCalendarCategoryDayHeaderCategoryTitle<>), typeof(BCalendarCategoryDayHeaderCategoryTitle<ICalendarCategory>));
        }
    }
}
