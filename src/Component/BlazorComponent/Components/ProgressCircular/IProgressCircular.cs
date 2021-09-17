using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public interface IProgressCircular : IHasProviderComponent
    {
        Dictionary<string, object> SvgAttributes => new();

        Dictionary<string, object> CircleAttributes => new();

        RenderFragment ChildContent { get; }

        bool Indeterminate => default;

        string StrokeDashOffset => default;
    }
}
