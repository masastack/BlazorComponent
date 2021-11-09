using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ITooltip : IHasProviderComponent, IActivatable, IMenuable
    {
        ElementReference ContentRef { set; }

        string Transition { get; }

        RenderFragment ChildContent { get; }
    }
}