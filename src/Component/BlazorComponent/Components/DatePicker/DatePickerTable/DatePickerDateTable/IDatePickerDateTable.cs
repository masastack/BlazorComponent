namespace BlazorComponent
{
    public interface IDatePickerDateTable : IDatePickerTable
    {
        IEnumerable<string> WeekDays { get; }

        bool ShowWeek { get; }

        int DisplayedMonth { get; }

        int WeekDaysBeforeFirstDayOfTheMonth { get; }

        int GetWeekNumber(int dayInMonth);

        bool ShowAdjacentMonths { get; }
    }
}
