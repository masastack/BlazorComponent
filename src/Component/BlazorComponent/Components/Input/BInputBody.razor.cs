using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BInputBody : BDomComponentBase,IInputBody
    {
        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public MarkupString InnerHtml { get; set; }

        [Parameter]
        public bool ShowLabel { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string PlaceHolder { get; set; }

        [Parameter]
        public string Type { get; set; } = "text";

        [Parameter]
        public bool IsTextArea { get; set; }

        [Parameter]
        public bool IsTextField { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Rows { get; set; }

        

        public async Task HandleChange(ChangeEventArgs args)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(args.Value.ToString());
            }
        }
    }
}
