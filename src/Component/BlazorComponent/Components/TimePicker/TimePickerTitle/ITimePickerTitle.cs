using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface ITimePickerTitle : IHasProviderComponent
    {
        bool AmPmReadonly { get; }
        
        string AmText { get; }
        
        string PmText { get; }

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
