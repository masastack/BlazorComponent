namespace BlazorComponent
{
    public interface ISwitch : IInput<bool>, ISelectable, IRippleable
    {
        bool IsLoading { get; }

        string LeftText { get; }

        string RightText { get; }
    }
}