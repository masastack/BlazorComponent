using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTableButton<TDatePickerTable> where TDatePickerTable : IDatePickerTable
    {
        [Parameter]
        public DateOnly Date { get; set; }

        [Parameter]
        public bool IsFloating { get; set; }

        [Parameter]
        public bool IsOtherMonth { get; set; }

        public Func<DateOnly,Dictionary<string, object>> GetButtonAttrs => Component.GetButtonAttrs;

        public Func<DateOnly, string> Formatter => Component.Formatter;
    }
}
