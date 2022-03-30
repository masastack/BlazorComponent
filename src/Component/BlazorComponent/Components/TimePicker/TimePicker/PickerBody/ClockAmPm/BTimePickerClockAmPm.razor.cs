using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTimePickerClockAmPm<TTimePicker> where TTimePicker : ITimePicker
    {
        public EventCallback<MouseEventArgs> OnAmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnAmClickAsync);

        public EventCallback<MouseEventArgs> OnPmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnPmClickAsync);
    }
}
