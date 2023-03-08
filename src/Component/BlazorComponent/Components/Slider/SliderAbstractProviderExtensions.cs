namespace BlazorComponent
{
    public static class SliderAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplySliderDefault<TValue, TNumeric>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                    .ApplyInputDefault<double>()
                    .Merge(typeof(BInputDefaultSlot<,>), typeof(BSliderDefaultSlot<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderSlider<,,>), typeof(BSliderSlider<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderChildren<,,>), typeof(BSliderChildren<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderInput<,,>), typeof(BSliderInput<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderSteps<,,>), typeof(BSliderSteps<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderThumbContainer<,,>), typeof(BSliderThumbContainer<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderThumb<,,>), typeof(BSliderThumb<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderThumbLabel<,,>), typeof(BSliderThumbLabel<TValue, TNumeric, ISlider<TValue, TNumeric>>))
                    .Apply(typeof(BSliderTrackContainer<,,>), typeof(BSliderTrackContainer<TValue, TNumeric, ISlider<TValue, TNumeric>>));
        }
    }
}
