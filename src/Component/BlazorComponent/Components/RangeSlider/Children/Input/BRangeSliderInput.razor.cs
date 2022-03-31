namespace BlazorComponent
{
    public partial class BRangeSliderInput<TValue, TRangeSlider> where TRangeSlider : IRangeSlider<TValue>
    {
        public IList<TValue> InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;
    }
}

