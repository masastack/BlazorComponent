namespace BlazorComponent
{
    public static class CalendarWithEventsAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyCalendarWithEventsDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BCalendarWithEventsPlaceholder<>), typeof(BCalendarWithEventsPlaceholder<ICalendarWithEvents>))
                .Apply(typeof(BCalendarWithEventsSlotChildren<>), typeof(BCalendarWithEventsSlotChildren<ICalendarWithEvents>))
                .Apply(typeof(BCalendarWithEventsTimedContainer<>), typeof(BCalendarWithEventsTimedContainer<ICalendarWithEvents>))
                .Apply(typeof(BCalendarWithEventsMore<>), typeof(BCalendarWithEventsMore<ICalendarWithEvents>))
                .Apply(typeof(BCalendarWithEventsSlotChildrenDayBody<>), typeof(BCalendarWithEventsSlotChildrenDayBody<ICalendarWithEvents>));
        }
    }
}
