using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerMonthTableBody : BDomComponentBase, IDatePickerTableBody
    {
        [Parameter]
        public BDatePickerTable Component { get; set; }

        [Parameter]
        public EventCallback<int> OnMonthSelected { get; set; }

        [Parameter]
        public string Color { get; set; } = "accent";

        public bool IsSelected(int month)
        {
            return Component.DisplayedMonth == month && Component.DisplayedYear == Component.Value.Year;
        }

        protected virtual EventCallback<MouseEventArgs> HandleMonthClick(int month) => EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
        {
            if (OnMonthSelected.HasDelegate)
            {
                await OnMonthSelected.InvokeAsync(month);
            }
        });
    }
}
