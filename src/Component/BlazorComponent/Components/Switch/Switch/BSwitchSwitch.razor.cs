namespace BlazorComponent
{
    public partial class BSwitchSwitch<TInput, TValue> where TInput : ISwitch<TValue>
    {
        string? LeftText => Component.LeftText;

        string? RightText => Component.RightText;
    }
}
