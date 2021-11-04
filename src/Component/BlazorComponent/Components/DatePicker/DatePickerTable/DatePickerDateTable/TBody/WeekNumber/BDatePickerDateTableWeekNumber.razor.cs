using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerDateTableWeekNumber<TDatePickerDateTable> where TDatePickerDateTable : IDatePickerDateTable
    {
        [Parameter]
        public int WeekNumber { get; set; }
    }
}
