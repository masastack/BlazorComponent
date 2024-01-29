namespace BlazorComponent
{
    public interface ISwitch<TValue> : ISelectable<TValue>
    {
        bool IsLoading { get; }

        string? LeftText { get; }

        string? RightText { get; }
        
        string? LeftIcon { get; }

        string? RightIcon { get; }
    }
}