using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ISheet : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }
    }
}
