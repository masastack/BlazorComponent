namespace BlazorComponent
{
    public partial class BTextFieldCounter<TValue, TInput> where TInput : ITextField<TValue>
    {
        public bool HasCounter => Component.HasCounter;

        public RenderFragment? CounterContent => Component.CounterContent;
    }
}
