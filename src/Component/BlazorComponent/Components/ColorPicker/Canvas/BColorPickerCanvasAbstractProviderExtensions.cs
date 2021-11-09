namespace BlazorComponent
{
    public static class BColorPickerCanvasAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyColorPickerCanvasDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BColorPickerCanvasCanvas<>), typeof(BColorPickerCanvasCanvas<IColorPickerCanvas>))
                .Apply(typeof(BColorPickerCanvasDot<>), typeof(BColorPickerCanvasDot<IColorPickerCanvas>));
        }
    }
}
