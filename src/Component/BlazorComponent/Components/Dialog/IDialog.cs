using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public interface IDialog : IHasProviderComponent, IActivatable
    {
        ElementReference ContentRef { set; }

        ElementReference DialogRef { set; }

        string Transition { get; }

        RenderFragment ChildContent { get; }

        Dictionary<string, object> ContentAttrs { get; }

        bool ShowContent { get; }
    }
}