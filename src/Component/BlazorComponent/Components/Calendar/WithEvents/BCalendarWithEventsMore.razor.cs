using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarWithEventsMore<TCalendarWithEvents> where TCalendarWithEvents : ICalendarWithEvents
    {
        [Parameter]
        public CalendarDaySlotScope Day { get; set; }

        public ElementReference EventsRef
        {
            set
            {
                Component.EventsRef = value;
            }
        }
    }
}
