namespace BlazorComponent
{
    public static class BColorPickerEditAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyColorPickerEditDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BColorPickerInput<>), typeof(BColorPickerInput<IColorPickerEdit>));
        }
    }
}
