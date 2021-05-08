using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BCheckbox : BDomComponentBase
    {
        protected RenderFragment CheckedIconContent { get; set; }
        protected RenderFragment UncheckIconContent { get; set; }
        protected RenderFragment IndeterminateIconContent { get; set; }
        protected RenderFragment AnimationContent { get; set; }

        [Parameter]
        public bool Checked { get; set; }

        [Parameter]
        public EventCallback<bool> CheckedChanged { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected async Task Toggle(MouseEventArgs args)
        {
            Console.WriteLine("toggle..");

            if (Indeterminate) Indeterminate = false;

            await CheckedChanged.InvokeAsync(!Checked);
        }
    }
}
