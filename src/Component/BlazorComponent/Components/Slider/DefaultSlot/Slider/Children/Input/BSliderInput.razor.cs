namespace BlazorComponent
{
    public partial class BSliderInput<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public TValue? InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;
    }
}
