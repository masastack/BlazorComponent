using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDatePickerTitleYearBtn<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public string Year => Component.Year;

        public string YearIcon => Component.YearIcon;

        public EventCallback<MouseEventArgs> OnYearBtnClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnYearBtnClickAsync);
    }
}
