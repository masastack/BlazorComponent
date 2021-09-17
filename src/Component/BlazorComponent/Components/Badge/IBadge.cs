using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IBadge : IHasProviderComponent
    {
        string Transition => default;

        bool Dot => default;

        RenderFragment BadgeContent => default;

        StringNumber Content => default;

        string Icon => default;
    }
}
