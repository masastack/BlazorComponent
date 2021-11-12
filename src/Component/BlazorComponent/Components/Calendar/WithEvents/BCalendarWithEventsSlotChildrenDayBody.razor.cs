using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarWithEventsSlotChildrenDayBody<TCalendarWithEvents> where TCalendarWithEvents : ICalendarWithEvents
    {
        [Parameter]
        public CalendarDayBodySlotScope Day { get; set; }

        [Parameter]
        public Func<CalendarDaySlotScope, List<CalendarEventParsed>> Getter { get; set; }

        [Parameter]
        public Func<CalendarEventVisual, CalendarDayBodySlotScope, RenderFragment> Mapper { get; set; }

        [Parameter]
        public bool Timed { get; set; }

        [Parameter]
        public Func<CalendarDaySlotScope, List<CalendarEventParsed>, bool, bool, List<CalendarEventVisual>> Mode { get; set; }

        [Parameter]
        public bool CategoryMode { get; set; }
    }
}
