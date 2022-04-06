namespace BlazorComponent
{
    public partial class BDatePickerMonthTableTBody<TDatePickerMonthTable> where TDatePickerMonthTable : IDatePickerMonthTable
    {
        public int DisplayedYear => Component.DisplayedYear;
    }
}
