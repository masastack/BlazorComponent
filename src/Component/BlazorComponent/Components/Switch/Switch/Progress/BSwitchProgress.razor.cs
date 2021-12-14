namespace BlazorComponent
{
    public partial class BSwitchProgress<TInput> where TInput : ISwitch
    {
        private bool Visible => Component.IsLoading;
    }
}