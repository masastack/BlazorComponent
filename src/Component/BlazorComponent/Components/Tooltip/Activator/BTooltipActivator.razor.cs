using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTooltipActivator<TTooltip>
        where TTooltip : ITooltip
    {
        public RenderFragment ComputedActivatorContent => Component.ComputedActivatorContent;
    }
}