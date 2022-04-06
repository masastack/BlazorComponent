using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IPicker : IHasProviderComponent
    {
        RenderFragment TitleContent { get; }

        RenderFragment ActionsContent { get; }

        string Transition { get; }

        RenderFragment ChildContent { get; }

        bool NoTitle { get; }
    }
}
