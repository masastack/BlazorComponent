namespace BlazorComponent
{
    public static class SliderAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplySliderDefault<TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                    .ApplyInputDefault<double>()
                    .Merge(typeof(BInputDefaultSlot<,>), typeof(BSliderDefaultSlot<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderSlider<,>), typeof(BSliderSlider<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderChildren<,>), typeof(BSliderChildren<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderInput<,>), typeof(BSliderInput<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderSteps<,>), typeof(BSliderSteps<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderThumbContainer<,>), typeof(BSliderThumbContainer<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderThumb<,>), typeof(BSliderThumb<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderThumbLabel<,>), typeof(BSliderThumbLabel<TValue, ISlider<TValue>>))
                    .Apply(typeof(BSliderTrackContainer<,>), typeof(BSliderTrackContainer<TValue, ISlider<TValue>>));
        }
    }
}
