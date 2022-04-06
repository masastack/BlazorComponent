namespace BlazorComponent
{
    public static class DatePickerAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyDatePickerDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDatePickerYears<>), typeof(BDatePickerYears<IDatePicker>))
                .Apply(typeof(BDatePickerTableHeader<>), typeof(BDatePickerTableHeader<IDatePicker>))
                .Apply(typeof(BDatePickerDateTable<>), typeof(BDatePickerDateTable<IDatePicker>))
                .Apply(typeof(BDatePickerMonthTable<>), typeof(BDatePickerMonthTable<IDatePicker>));
        }
    }
}
