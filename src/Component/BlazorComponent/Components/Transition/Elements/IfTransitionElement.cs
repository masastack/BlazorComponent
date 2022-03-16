using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class IfTransitionElement : ToggleableTransitionElement
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (LazyValue)
            {
                base.BuildRenderTree(builder);
            }
        }
    }
}
