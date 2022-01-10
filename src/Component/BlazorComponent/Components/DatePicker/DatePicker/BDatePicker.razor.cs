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

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

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
