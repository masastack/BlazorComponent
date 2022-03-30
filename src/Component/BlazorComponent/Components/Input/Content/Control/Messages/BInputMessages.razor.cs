namespace BlazorComponent
{
    public partial class BInputMessages<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        public bool ShowDetails => Component.ShowDetails;
    }
}
