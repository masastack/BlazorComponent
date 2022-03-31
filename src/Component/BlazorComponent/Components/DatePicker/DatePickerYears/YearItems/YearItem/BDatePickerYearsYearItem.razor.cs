using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDatePickerYearsYearItem<TDatePickerYears> where TDatePickerYears : IDatePickerYears
    {
        [Parameter]
        public int Year { get; set; }

        public Func<DateOnly, string> Formatter => Component.Formatter;

        public int Value => Component.Value;

        public EventCallback<MouseEventArgs> HandleOnYearItemClick => CreateEventCallback<MouseEventArgs>(args => Component.HandleOnYearItemClickAsync(Year));
    }
}
