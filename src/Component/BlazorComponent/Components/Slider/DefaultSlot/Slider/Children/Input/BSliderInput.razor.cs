namespace BlazorComponent
{
    public partial class BSliderInput<TValue, TInput> where TInput : ISlider<TValue>
    {
        public TValue InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;
    }
}

