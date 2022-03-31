namespace BlazorComponent
{
    public partial class BDatePickerDateTableTHead<TDatePickerDateTable> where TDatePickerDateTable : IDatePickerDateTable
    {
        public bool ShowWeek => Component.ShowWeek;

        public IEnumerable<string> WeekDays => Component.WeekDays;
    }
}
