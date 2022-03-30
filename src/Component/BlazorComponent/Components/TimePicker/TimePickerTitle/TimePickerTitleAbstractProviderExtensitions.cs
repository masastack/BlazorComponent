namespace BlazorComponent
{
    public static class TimePickerTitleAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyTimePickerTitleDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                        .Apply(typeof(BTimePickerTitleTime<>), typeof(BTimePickerTitleTime<ITimePickerTitle>))
                        .Apply(typeof(BTimePickerTitleAmPm<>), typeof(BTimePickerTitleAmPm<ITimePickerTitle>))
                        .Apply(typeof(BPickerTitlePickerButton<>), typeof(BPickerTitlePickerButton<ITimePickerTitle>));
        }
    }
}
