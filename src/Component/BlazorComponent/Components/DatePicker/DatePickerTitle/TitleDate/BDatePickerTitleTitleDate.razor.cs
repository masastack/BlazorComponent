using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDatePickerTitleTitleDate<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public EventCallback<MouseEventArgs> OnTitleDateBtnClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnTitleDateBtnClickAsync);
    }
}
