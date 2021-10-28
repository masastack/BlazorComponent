using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ITooltip : IHasProviderComponent, IActivatable
    {
        ElementReference ContentRef { set; }

        string Transition { get; }

        RenderFragment ChildContent { get; }
    }
}