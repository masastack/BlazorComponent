namespace BlazorComponent
{
    public interface ISwitch : ISelectable
    {
        bool IsLoading { get; }

        string LeftText { get; }

        string RightText { get; }
    }
}