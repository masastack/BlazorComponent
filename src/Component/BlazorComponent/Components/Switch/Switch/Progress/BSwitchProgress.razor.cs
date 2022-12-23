namespace BlazorComponent
{
    public partial class BSwitchProgress<TInput, TValue> where TInput : ISwitch<TValue>
    {
        private bool Visible => Component.IsLoading;
    }
}