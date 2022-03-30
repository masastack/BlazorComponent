using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IOverlay : IHasProviderComponent
    {
        bool Value { get; }

        RenderFragment ChildContent { get; }
    }
}
