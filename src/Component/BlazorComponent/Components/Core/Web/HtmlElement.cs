using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace BlazorComponent.Web
{
    public class HtmlElement
    {
        public HtmlElement()
        {
        }

        public HtmlElement(IJSRuntime js, string selectors)
        {
            JS = js;
            Selectors = selectors;
        }

        public IJSRuntime JS { get; internal set; }

        public string Selectors { get; internal set; }

        public async Task DispatchEventAsync(Event @event)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.TriggerEvent, Selectors, @event.Type, @event.Name, @event.ShouldStopPropagation);
        }

        public async Task SetPropertyAsync(string name, object value)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.SetProperty, Selectors, name, value);

        }

        public async Task<BoundingClientRect> GetBoundingClientRectAsync()
        {
            return await JS.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Selectors);
        }

        public async Task AddEventListenerAsync<T>(string type, EventCallback<T> listener, OneOf<EventListenerOptions, bool> options)
            where T : EventArgs
        {
            await JS.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener, Selectors, type, DotNetObjectReference.Create(new Invoker<T>(async (p) =>
            {
                if (listener.HasDelegate)
                {
                    await listener.InvokeAsync(p);
                }
            })), options.Value);
        }

        public async Task RemoveEventListenerAsync(string type)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, Selectors, type);
        }
    }
}
