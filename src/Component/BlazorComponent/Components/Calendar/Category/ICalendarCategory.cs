using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ICalendarCategory : ICalendarDaily
    {
        RenderFragment<CategoryContentProps> CategoryContent { get; }

        List<OneOf<string, Dictionary<string, object>>> ParsedCategories() => default;

        CategoryContentProps GetCategoryScope(CategoryContentProps scope,
            OneOf<string, Dictionary<string, object>> category);

        List<List<CalendarTimestamp>> Intervals() => default;
        
        CalendarDayBodySlotScope GetSlotScope(CalendarTimestamp timestamp) => default;
    }
}
