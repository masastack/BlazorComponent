using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDayHeaderCategory<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        [Parameter]
        public CategoryContentProps Scpoe { get; set; }

        public RenderFragment<CategoryContentProps> CategoryContent => Component.CategoryContent;

        public CalendarDaySlotScope DayHeaderScpoe => new CalendarDaySlotScope(false, Index, Scpoe.Week, Day);

        public string HeaderTitle
        {
            get 
            {
                if (Scpoe.Category.IsT1)
                {
                    if (Scpoe.Category.AsT1.TryGetValue(CalendarParser.CalendarCategoryCategoryName, out var nameObj))
                        return nameObj.ToString();
                }

                return Scpoe.Category.AsT0;
            }
        }
    }
}
