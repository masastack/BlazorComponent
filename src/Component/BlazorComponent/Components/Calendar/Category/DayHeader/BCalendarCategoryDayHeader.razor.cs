using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDayHeader<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public RenderFragment<CalendarDaySlotScope> DayHeaderContent { get; set; }

        public List<OneOf<string, Dictionary<string, object>>> ParsedCategories => Component.ParsedCategories();

        public CategoryContentProps Scpoe => new(Component.Days, Day);

        public CategoryContentProps GetCategoryScope(OneOf<string, Dictionary<string, object>> category) =>
            Component.GetCategoryScope(Scpoe, category);
    }
}
