using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ITimePickerTitle : IHasProviderComponent
    {
        bool AmPmReadonly { get; }

        TimePeriod Period { get; }

        string DisplayHour { get; }

        string DisplayMinute { get; }

        string DisplaySecond { get; }

        bool UseSeconds { get; }

        Task HandleOnAmClickAsync(MouseEventArgs args);

        Task HandleOnPmClickAsync(MouseEventArgs args);

        Task HandleOnHourClickAsync(MouseEventArgs args);

        Task HandleOnMinuteClickAsync(MouseEventArgs args);

        Task HandleOnSecondClickAsync(MouseEventArgs args);
    }
}
