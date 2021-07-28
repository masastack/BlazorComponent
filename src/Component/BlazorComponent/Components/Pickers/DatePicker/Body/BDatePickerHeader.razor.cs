using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerHeader : BDomComponentBase
    {
        [Parameter]
        public string ActivePicker { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public DateTime Value { get; set; }

        [Parameter]
        public DateTime? Min { get; set; }

        [Parameter]
        public DateTime? Max { get; set; }

        [Obsolete("Use OnPrevClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> PrevClick { get; set; }

        [Obsolete("Use OnNextClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> NextClick { get; set; }

        [Obsolete("Use OnDateClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> DateClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnPrevClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnNextClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnDateClick { get; set; }

        public string DisplayValue => ActivePicker == "DATE" ? $"{Value.Year.ToString().PadLeft(4, '0')}-{Value.Month.ToString().PadLeft(2, '0')}" : Value.Year.ToString().PadLeft(4, '0');

        public bool PrevDisabled => Min != null && (ActivePicker == "DATE" ? (Value.Year == Min.Value.Year && Value.Month <= Min.Value.Month) : (Value.Year <= Min.Value.Year));

        public bool NextDisabled => Max != null && (ActivePicker == "DATE" ? (Value.Year == Max.Value.Year && Value.Month >= Max.Value.Month) : (Value.Year >= Max.Value.Year));

        protected override void OnParametersSet()
        {
            if (PrevClick.HasDelegate)
            {
                OnPrevClick = PrevClick;
            }

            if (NextClick.HasDelegate)
            {
                OnNextClick = NextClick;
            }

            if (DateClick.HasDelegate)
            {
                OnDateClick = DateClick;
            }
        }
    }
}
