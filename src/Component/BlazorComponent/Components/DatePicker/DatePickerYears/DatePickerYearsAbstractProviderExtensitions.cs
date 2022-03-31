namespace BlazorComponent
{
    public static class DatePickerYearsAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyDatePickerYearsDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                 .Apply(typeof(BDatePickerYearsYearItems<>), typeof(BDatePickerYearsYearItems<IDatePickerYears>))
                 .Apply(typeof(BDatePickerYearsYearItem<>), typeof(BDatePickerYearsYearItem<IDatePickerYears>));
        }
    }
}
