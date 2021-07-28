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

        [Parameter]
        public DateTime? Min { get; set; }

        [Parameter]
        public DateTime? Max { get; set; }

        public bool IsSelected(int month)
        {
            return Component.Value.Month == month && Component.DisplayedYear == Component.Value.Year;
        }

        public bool IsDisabled(int month)
        {
            return (Min != null && Component.DisplayedYear == Min.Value.Year && month < Min.Value.Month) || (Max != null && Component.DisplayedYear == Max.Value.Year && month > Max.Value.Month);
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
