using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerHeaderHeader<TDatePickerHeader> where TDatePickerHeader : IDatePickerHeader
    {
        public string Transition => Component.Transition;

        public DateOnly Value => Component.Value;

        public RenderFragment ComponentChildContent => Component.ChildContent;

        public Dictionary<string, object> HeaderAttrs => Component.ButtonAttrs;

        public Func<DateOnly, string> Formatter => Component.Formatter;
    }
}
