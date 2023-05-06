namespace BlazorComponent
{
    public interface ITimePicker : IHasProviderComponent
    {
        SelectingTimes Selecting { get; }

        bool AmPmInTitle { get; }
        
        string? AmText { get; }
        
        string? PmText { get; }

        bool IsAmPm { get; }

        Task HandleOnAmClickAsync(MouseEventArgs args);

        Task HandleOnPmClickAsync(MouseEventArgs args);
    }
}
