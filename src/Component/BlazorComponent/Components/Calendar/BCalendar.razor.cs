using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendar : BCalendarEvents
    {
        #region Slot

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<CategoryContentProps> CategoryContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDaySlotScope> DayContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDayBodySlotScope> DayBodyContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDaySlotScope> DayHeaderContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarTimestamp> DayLabelContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarTimestamp> DayLabelHeaderContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarTimestamp> DayMonthContent { get; set; }

        [Parameter]
        public RenderFragment<CalendarDayBodySlotScope> IntervalContent { get; set; }

        #endregion

        public virtual CalendarRenderProps RenderProps { get; }

        public ElementReference CalendarRef { get; set; }

        protected IReadOnlyDictionary<string, object> Props { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            Props = parameters.ToDictionary();

            return base.SetParametersAsync(parameters);
        }
    }
}
