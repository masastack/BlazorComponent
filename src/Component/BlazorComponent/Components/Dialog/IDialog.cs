using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDialog : IHasProviderComponent
    {
        ElementReference ContentRef { set; }

        ElementReference DialogRef { set; }

        bool Value { get; }

        EventCallback<bool> ValueChanged { get; }

        string Transition { get; }

        RenderFragment ChildContent { get; }

        RenderFragment<ActivatorProps> ActivatorContent { get; }

        Guid ActivatorId { get; }

        Dictionary<string, object> ActivatorAttrs { get; }

        Dictionary<string, object> ContentAttrs { get; }
    }
}
