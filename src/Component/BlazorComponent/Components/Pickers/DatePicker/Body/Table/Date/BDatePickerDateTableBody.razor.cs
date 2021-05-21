using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerDateTableBody : BDomComponentBase, IDatePickerTableBody
    {
        private const string CURRENT_BTN = "current-btn";

        [Parameter]
        public bool ShowWeek { get; set; }

        [Parameter]
        public BDatePickerTable Component { get; set; }

        [Parameter]
        public EventCallback<int> OnDaySelected { get; set; }

        protected int DaysInMonth => new DateTime(Component.DisplayedYear, Component.DisplayedMonth, 1).AddMonths(1).AddDays(-1).Day;

        protected int WeekDaysBeforeFirstDayOfTheMonth => (int)new DateTime(Component.DisplayedYear, Component.DisplayedMonth, 1).DayOfWeek;

        public List<string> WeekDays => new()
        {
            "S",
            "M",
            "T",
            "W",
            "T",
            "F",
            "S"
        };

        private bool IsCurrentDay(int day)
        {
            var now = DateTime.Now;
            return day == now.Day && Component.DisplayedMonth == now.Month && Component.DisplayedYear == now.Year;
        }

        public bool IsSelected(int day)
        {
            return day == Component.Value.Day && Component.DisplayedMonth == Component.Value.Month && Component.DisplayedYear == Component.Value.Year;
        }

        protected virtual EventCallback<MouseEventArgs> HandleDayClick(int day) => EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
        {
            if (OnDaySelected.HasDelegate)
            {
                await OnDaySelected.InvokeAsync(day);
            }
        });
    }
}
