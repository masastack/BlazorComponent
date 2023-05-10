namespace BlazorComponent;

public interface IScrollable
{
    bool CanScroll { get; }

    string? ScrollTarget { get; }

    double ScrollThreshold { get; }

    IJSRuntime Js { get; }
}