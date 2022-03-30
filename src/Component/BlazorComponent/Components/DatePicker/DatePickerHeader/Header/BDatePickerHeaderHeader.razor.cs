using Microsoft.AspNetCore.Components;

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
