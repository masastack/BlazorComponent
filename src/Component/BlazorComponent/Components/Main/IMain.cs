using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IMain : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }
    }
}
