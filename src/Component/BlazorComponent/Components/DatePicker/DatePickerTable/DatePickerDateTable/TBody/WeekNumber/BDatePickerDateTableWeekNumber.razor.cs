using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDatePickerDateTableWeekNumber<TDatePickerDateTable> where TDatePickerDateTable : IDatePickerDateTable
    {
        [Parameter]
        public int WeekNumber { get; set; }
    }
}
