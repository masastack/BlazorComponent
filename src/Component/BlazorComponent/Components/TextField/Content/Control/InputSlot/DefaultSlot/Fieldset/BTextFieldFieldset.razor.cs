namespace BlazorComponent
{
    public partial class BTextFieldFieldset<TValue, TInput> where TInput : ITextField<TValue>
    {
        public bool Outlined => Component.Outlined;
    }
}
