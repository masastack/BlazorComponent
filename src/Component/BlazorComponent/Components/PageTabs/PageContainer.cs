using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PageContainer : ComponentBase
    {
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override bool ShouldRender()
        {
            return Value;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }
    }
}
