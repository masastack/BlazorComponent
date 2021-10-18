using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface ITooltip : IHasProviderComponent
    {
        ElementReference ContentRef { set; }

        bool Value { get; }

        EventCallback<bool> ValueChanged { get; }

        string Transition { get; }

        RenderFragment ChildContent { get; }

        RenderFragment<ActivatorProps> ActivatorContent { get; }

        Guid ActivatorId { get; }

        Dictionary<string, object> ActivatorAttrs { get; } 
    }
}
