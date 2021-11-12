using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BCalendarWithEventsPlaceholder<TCalendarWithEvents> where TCalendarWithEvents : ICalendarWithEvents
    {
        [Parameter]
        public CalendarDaySlotScope Day { get; set; }
    }
}
