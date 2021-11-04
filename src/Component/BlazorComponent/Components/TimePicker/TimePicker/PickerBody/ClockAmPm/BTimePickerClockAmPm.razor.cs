using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTimePickerClockAmPm<TTimePicker> where TTimePicker : ITimePicker
    {
        public EventCallback<MouseEventArgs> OnAmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnAmClickAsync);

        public EventCallback<MouseEventArgs> OnPmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnPmClickAsync);
    }
}
