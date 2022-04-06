using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IProgressCircular : IHasProviderComponent
    {
        Dictionary<string, object> SvgAttrs => new();

        Dictionary<string, object> CircleAttrs => new();

        RenderFragment ChildContent { get; }

        bool Indeterminate => default;

        string StrokeDashOffset => default;
    }
}
