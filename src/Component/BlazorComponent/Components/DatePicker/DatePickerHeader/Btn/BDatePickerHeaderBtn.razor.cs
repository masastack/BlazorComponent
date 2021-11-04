using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerHeaderBtn<TDatePickerHeader>
        where TDatePickerHeader : IDatePickerHeader
    {
        [Parameter]
        public int Change { get; set; }

        public bool RTL => Component.RTL;

        public string PrevIcon => Component.PrevIcon;

        public string NextIcon => Component.NextIcon;
    }
}
