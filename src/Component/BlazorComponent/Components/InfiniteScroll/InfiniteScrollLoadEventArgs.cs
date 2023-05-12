namespace BlazorComponent;

public class InfiniteScrollLoadEventArgs : EventArgs
{
    public InfiniteScrollLoadStatus Status { get; set; }
}

public enum InfiniteScrollLoadStatus
{
    Ok,
    Error,
    Empty,
    Loading
}
