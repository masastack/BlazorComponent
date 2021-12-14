using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

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
