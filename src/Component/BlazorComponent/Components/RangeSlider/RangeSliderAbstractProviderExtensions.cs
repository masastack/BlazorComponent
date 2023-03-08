namespace BlazorComponent
{
    public static class RangeSliderAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyRangeSliderDefault<TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BSliderThumbContainer<,,>), typeof(BRangeSliderThumbContainer<TValue, IRangeSlider<TValue>>))
                .Merge(typeof(BSliderInput<,,>), typeof(BRangeSliderInput<TValue, IRangeSlider<TValue>>))
                .Merge(typeof(BSliderTrackContainer<,,>), typeof(BRangeSliderTrackContainer<TValue, IRangeSlider<TValue>>));
        }
    }
}
