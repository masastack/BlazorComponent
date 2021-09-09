using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent.Web
{
    public class HtmlElement : JsObject
    {
        public async Task DispatchEventAsync(Event @event)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.TriggerEvent, ElementReference, @event.Type, @event.Name, @event.ShouldStopPropagation);
        }

        public async Task SetPropertyAsync(string name, object value)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.SetProperty, ElementReference, name, value);
        }
    }
}
