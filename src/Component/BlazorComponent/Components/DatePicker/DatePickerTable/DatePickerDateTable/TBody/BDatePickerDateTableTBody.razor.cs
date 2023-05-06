namespace BlazorComponent
{
    public partial class BDatePickerDateTableTBody<TDatePickerDateTable> where TDatePickerDateTable : IDatePickerDateTable
    {
        public int DisplayedYear => Component.DisplayedYear;

        public int DisplayedMonth => Component.DisplayedMonth;

        public int WeekDaysBeforeFirstDayOfTheMonth => Component.WeekDaysBeforeFirstDayOfTheMonth;

        public bool ShowWeek => Component.ShowWeek;

        public Func<int, int> GetWeekNumber => Component.GetWeekNumber;

        public bool ShowAdjacentMonths => Component.ShowAdjacentMonths;

        public List<List<(string Type, Dictionary<string, object?> Attrs)>> Children
        {
            get
            {
                var children = new List<List<(string Type, Dictionary<string, object> Attrs)>>();
                var daysInMonth = DateTime.DaysInMonth(DisplayedYear, DisplayedMonth + 1);
                var rows = new List<(string Type, Dictionary<string, object> Attrs)>();
                var day = WeekDaysBeforeFirstDayOfTheMonth;

                if (ShowWeek)
                {
                    rows.Add(("WeekNumber", new Dictionary<string, object>
                    {
                        {"WeekNumber",GetWeekNumber(1)}
                    }));
                }

                var prevMonthYear = DisplayedMonth > 0 ? DisplayedYear : DisplayedYear - 1;
                var prevMonth = (DisplayedMonth + 11) % 12;
                var firstDayFormPreviousMonth = DateTime.DaysInMonth(DisplayedMonth > 0 ? DisplayedYear : DisplayedYear - 1, DisplayedMonth == 0 ? 12 : DisplayedMonth);
                var cellsInRow = ShowWeek ? 8 : 7;

                while (day-- > 0)
                {
                    var date = new DateOnly(prevMonthYear, prevMonth + 1, firstDayFormPreviousMonth - day);
                    rows.Add(("ShowAdjacentMonthsButton", new Dictionary<string, object>
                    {
                        {"Date",date},
                        {"IsFloating",true},
                        {"IsOtherMonth",true}
                    }));
                }

                for (day = 1; day <= daysInMonth; day++)
                {
                    var date = new DateOnly(DisplayedYear, DisplayedMonth + 1, day);
                    rows.Add(("Button", new Dictionary<string, object>
                    {
                        {"Date",date},
                        {"IsFloating",true}
                    }));

                    if (rows.Count % cellsInRow == 0)
                    {
                        children.Add(rows);
                        rows = new List<(string Type, Dictionary<string, object> Attrs)>();
                        if (ShowWeek && (day < daysInMonth || ShowAdjacentMonths))
                        {
                            rows.Add(("WeekNumber", new Dictionary<string, object>
                            {
                                {"WeekNumber",GetWeekNumber(day+7)}
                            }));
                        }
                    }
                }

                var nextMonthYear = DisplayedMonth == 11 ? DisplayedYear + 1 : DisplayedYear;
                var nextMonth = (DisplayedMonth + 1) % 12;
                var nextMonthDay = 1;

                while (rows.Count < cellsInRow)
                {
                    var date = new DateOnly(nextMonthYear, nextMonth + 1, nextMonthDay++);
                    rows.Add(("ShowAdjacentMonthsButton", new Dictionary<string, object>
                    {
                        {"Date",date},
                        {"IsFloating",true},
                        {"IsOtherMonth",true}
                    }));
                }

                if (rows.Count > 0)
                {
                    children.Add(rows);
                }

                return children;
            }
        }
    }
}
