using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerDateTableTR<TDatePickerDateTable> where TDatePickerDateTable : IDatePickerDateTable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
