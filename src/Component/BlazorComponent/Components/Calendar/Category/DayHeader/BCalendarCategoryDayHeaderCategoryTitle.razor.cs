using Microsoft.AspNetCore.Components;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarCategoryDayHeaderCategoryTitle<TCalendarCategory> where TCalendarCategory : ICalendarCategory
    {
        [Parameter]
        public string HeaderTitle { get; set; }
    }
}
