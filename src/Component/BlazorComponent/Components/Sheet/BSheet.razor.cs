using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSheet : BDomComponentBase, IThemeable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public virtual string Tag { get; set; } = "div";

        public virtual bool IsDark { get; }

        public bool Dark { get ; set ; }

        public bool Light { get ; set ; }
    }
}
