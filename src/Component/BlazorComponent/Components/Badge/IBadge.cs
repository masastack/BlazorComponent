using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IBadge : IHasProviderComponent
    {
        string Transition { get; }

        bool Dot { get; }

        RenderFragment BadgeContent { get; }

        StringNumber Content { get; }

        string Icon { get; }

        bool Value { get; }
    }
}
