using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IButton : IHasProviderComponent
    {
        bool Block { get; }

        RenderFragment ChildContent { get; }

        string? Color { get; }

        bool Disabled { get; }

        StringNumber? Height { get; }

        RenderFragment LoaderContent { get; }

        bool Loading { get; }

        StringNumber? MaxHeight { get; }

        StringNumber? MaxWidth { get; }

        StringNumber? MinHeight { get; }

        StringNumber? MinWidth { get; }

        bool Outlined { get; }

        StringNumber? Width { get; }
    }
}