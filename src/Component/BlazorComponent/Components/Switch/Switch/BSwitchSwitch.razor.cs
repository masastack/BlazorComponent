namespace BlazorComponent
{
    public partial class BSwitchSwitch<TInput> where TInput : ISwitch
    {
        string LeftText => Component.LeftText;

        string RightText => Component.RightText;
    }
}
