namespace BlazorComponent
{
    public static class BColorPickerAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyColorPickerDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BColorPickerControls<>), typeof(BColorPickerControls<IColorPicker>));
        }
    }
}
