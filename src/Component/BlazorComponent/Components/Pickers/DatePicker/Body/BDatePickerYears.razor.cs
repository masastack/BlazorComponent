using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerYears : BDomComponentBase
    {
        [Parameter]
        public StringNumber Min { get; set; }

        [Parameter]
        public StringNumber Max { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public StringNumber Value { get; set; }

        protected int SelectedYear => Value != null ? Value.ToInt32() : DateTime.Now.Year;

        protected int MaxYear => Max != null ? Max.ToInt32() : SelectedYear + 100;

        protected int MinYear => Math.Min(MaxYear, Min != null ? Min.ToInt32() : SelectedYear - 100);

        [Parameter]
        public EventCallback<int> OnYearSelected { get; set; }

        public ElementReference ActiveRef { get; set; }

        protected virtual async Task HandleYearClick(int year)
        {
            if (OnYearSelected.HasDelegate)
            {
                await OnYearSelected.InvokeAsync(year);
            }
        }
    }
}
