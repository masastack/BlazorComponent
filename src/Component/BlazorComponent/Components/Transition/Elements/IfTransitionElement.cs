using Microsoft.AspNetCore.Components.Rendering;

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
