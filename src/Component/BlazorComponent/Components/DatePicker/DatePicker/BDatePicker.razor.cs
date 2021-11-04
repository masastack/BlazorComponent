using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePicker
    {
        [Parameter]
        public bool NoTitle { get; set; }

        [Parameter]
        public DatePickerType Type { get; set; } = DatePickerType.Date;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected DatePickerType InternalActivePicker
        {
            get
            {
                return GetValue<DatePickerType>();
            }
            set
            {
                SetValue(value);
            }
        }
    }
}
