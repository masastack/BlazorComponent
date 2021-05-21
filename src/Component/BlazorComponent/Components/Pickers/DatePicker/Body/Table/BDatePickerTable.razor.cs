using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTable : BDomComponentBase
    {
        [Parameter]
        public DateTime Value { get; set; }

        [Parameter]
        public DateTime TableDate { get; set; }

        public int DisplayedMonth => TableDate.Month;

        public int DisplayedYear => TableDate.Year;
    }
}
