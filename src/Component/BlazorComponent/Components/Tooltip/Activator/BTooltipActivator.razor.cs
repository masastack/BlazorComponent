using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTooltipActivator<TTooltip>
        where TTooltip : ITooltip
    {
        protected RenderFragment ComputedActivatorContent => Component.ComputedActivatorContent;
    }
}