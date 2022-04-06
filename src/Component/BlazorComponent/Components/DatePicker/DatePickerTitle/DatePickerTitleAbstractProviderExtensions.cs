namespace BlazorComponent
{
    public static class DatePickerTitleAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDatePickerTitleDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDatePickerTitleYearBtn<>), typeof(BDatePickerTitleYearBtn<IDatePickerTitle>))
                .Apply(typeof(BDatePickerTitleTitleDate<>), typeof(BDatePickerTitleTitleDate<IDatePickerTitle>))
                .Apply(typeof(BPickerTitlePickerButton<>), typeof(BPickerTitlePickerButton<IDatePickerTitle>))
                .Apply(typeof(BDatePickerTitleYearIcon<>), typeof(BDatePickerTitleYearIcon<IDatePickerTitle>))
                .Apply(typeof(BDatePickerTitleTitleText<>), typeof(BDatePickerTitleTitleText<IDatePickerTitle>));
        }
    }
}
