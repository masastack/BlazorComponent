using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IBanner : IHasProviderComponent
    {
        bool HasIcon => default;

        string Icon => default;

        RenderFragment ChildContent { get; }

        RenderFragment IconContent { get; }

        RenderFragment ComputedActionsContent { get; }
    }
}
