using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ITimelineItem : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }

        bool HideDot => default;

        string Icon => default;

        RenderFragment IconContent { get; }

        RenderFragment OppositeContent { get; }
    }
}
