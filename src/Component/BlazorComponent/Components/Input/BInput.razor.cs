using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BInput : BDomComponentBase
    {
        [Parameter]
        public RenderFragment Prepend { get; set; }

        [Parameter]
        public RenderFragment Append { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public List<string> Messages { get; set; }

        protected bool Blur { get; set; }

        protected bool ShowDetails => Messages?.Count > 0;

        protected EventCallback<MouseEventArgs> HandleClick { get; set; }

        protected EventCallback HandleBlur { get; set; }
    }
}
