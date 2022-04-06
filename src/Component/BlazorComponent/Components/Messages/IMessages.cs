using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IMessages : IHasProviderComponent
    {
        List<string> Value { get; }

        RenderFragment<string> ChildContent { get; }
    }
}
