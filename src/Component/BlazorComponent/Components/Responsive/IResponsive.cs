using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IResponsive : IAbstractComponent
    {
        StringNumber AspectRatio { get; }

        string ContentClass { get; }

        StringNumber Height { get; }

        StringNumber MaxHeight { get; }

        StringNumber MinHeight { get; }

        StringNumber Width { get; }

        StringNumber MaxWidth { get; }

        StringNumber MinWidth { get; }

        RenderFragment ChildContent { get; }
    }
}