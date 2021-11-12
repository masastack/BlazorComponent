using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDay<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        [Parameter]
        public CalendarTimestamp Day { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public int CalendarIndex { get; set; }

        public OneOf<string, Dictionary<string, object>> Category => Component.ParsedCategories()[CalendarIndex];
    }
}
