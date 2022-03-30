namespace BlazorComponent
{
    public static class DatePickerHeaderAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyDatePickerHeaderDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                 .Apply(typeof(BDatePickerHeaderHeader<>), typeof(BDatePickerHeaderHeader<IDatePickerHeader>))
                 .Apply(typeof(BDatePickerHeaderBtn<>), typeof(BDatePickerHeaderBtn<IDatePickerHeader>));
        }
    }
}
