using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BWrapComponent : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public string StaticClass { get; set; }

        public string StaticStyle { get; set; }

        public override void SetComponentClass()
        {
            CssBuilder
                .Add(() => StaticClass);
        }
    }
}
