using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BProcessLinear : BDomComponentBase
    {
        [Parameter]
        public bool Stream { get; set; }

        [Parameter]
        public string Background { get; set; }

        [Parameter]
        public string Buffer { get; set; }

        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Indeterminate { get; set; }

        public bool Determinate { get; set; }

        protected string Long { get; set; }

        protected string Short { get; set; }
    }
}
