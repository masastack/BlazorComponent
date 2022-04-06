namespace BlazorComponent
{
    public partial class BDatePickerYearsYearItems<TDatePickerYears> where TDatePickerYears : IDatePickerYears
    {
        public int Value => Component.Value;

        public DateOnly? Max => Component.Max;

        public DateOnly? Min => Component.Min;
    }
}
