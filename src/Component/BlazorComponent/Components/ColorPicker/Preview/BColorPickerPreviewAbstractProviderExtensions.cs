namespace BlazorComponent
{
    public static class BColorPickerPreviewAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyColorPickerPreviewDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BColorPickerDot<>), typeof(BColorPickerDot<IColorPickerPreview>))
                .Apply(typeof(BColorPickerSliders<>), typeof(BColorPickerSliders<IColorPickerPreview>))
                .Apply(typeof(BColorPickerHue<>), typeof(BColorPickerHue<IColorPickerPreview>))
                .Apply(typeof(BColorPickerTrack<>), typeof(BColorPickerTrack<IColorPickerPreview>))
                .Apply(typeof(BColorPickerAlpha<>), typeof(BColorPickerAlpha<IColorPickerPreview>));
        }
    }
}
