using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ITooltip : IHasProviderComponent, IActivatable, IMenuable
    {
        ElementReference ContentElement { set; }

        string Transition { get; }

        RenderFragment ChildContent { get; }

        RenderFragment<ActivatorProps> ActivatorContent { get; }
    }
}