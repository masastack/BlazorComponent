using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTimePickerTitleTime<TTimePickerTitle> where TTimePickerTitle : ITimePickerTitle
    {
        public string DisplayedHour => Component.DisplayHour;

        public string DisplayedMinute => Component.DisplayMinute;

        public string DisplayedSecond => Component.DisplaySecond;

        public bool UseSeconds => Component.UseSeconds;

        public EventCallback<MouseEventArgs> OnHourClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnHourClickAsync);

        public EventCallback<MouseEventArgs> OnMinuteClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnMinuteClickAsync);

        public EventCallback<MouseEventArgs> OnSecondClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnSecondClickAsync);
    }
}
