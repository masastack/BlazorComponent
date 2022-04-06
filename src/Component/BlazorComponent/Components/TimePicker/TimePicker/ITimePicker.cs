using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface ITimePicker : IHasProviderComponent
    {
        SelectingTimes Selecting { get; }

        bool AmPmInTitle { get; }

        bool IsAmPm { get; }

        Task HandleOnAmClickAsync(MouseEventArgs args);

        Task HandleOnPmClickAsync(MouseEventArgs args);
    }
}
