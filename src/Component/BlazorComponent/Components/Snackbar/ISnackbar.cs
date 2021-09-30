using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface ISnackbar : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }

        string Action => default;

        RenderFragment ActionContent { get; }
    }
}
