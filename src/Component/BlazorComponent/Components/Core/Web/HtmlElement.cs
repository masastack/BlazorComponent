﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public HtmlElement(IJSRuntime js, string selector)
        {
            JS = js;
            Selector = selector;
        }

        public IJSRuntime JS { get; internal set; }

        public string Selector { get; internal set; } // TODO: may be singular: Selector

        public HtmlElement ParentElement => new HtmlElement(JS, $"{Selector}.parentElement");

        public HtmlElement OffsetParent => new HtmlElement(JS, $"{Selector}.offsetParent");

        public async Task DispatchEventAsync(Event @event)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.TriggerEvent, Selector, @event.Type, @event.Name, @event.ShouldStopPropagation);
        }

        public async Task SetPropertyAsync(string name, object value)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.SetProperty, Selector, name, value);
        }

        public async Task<BoundingClientRect> GetBoundingClientRectAsync()
        {
            return await JS.InvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, Selector, ".m-application");
        }

        public async Task<Dictionary<string, string>> GetStylesAsync(params string[] styleNames)
        {
            Dictionary<string, string> styles = new();

            foreach (var styleName in styleNames)
            {
                var styleValue = await JS.InvokeAsync<string>(JsInteropConstants.GetStyle, Selector, styleName);
                styles.Add(styleName, styleValue);
            }

            return styles;
        }

        public async Task SetStylesAsync(params(string name, string value)[] styles)
        {
            await JS.InvokeAsync<string>(JsInteropConstants.SetStyle, styles.ToDictionary(s => s.name, s => s.value));
        }

        public async Task<Element> GetDomInfoAsync()
        {
            return await JS.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, Selector);
        }

        public async Task AddEventListenerAsync(string type, EventCallback listener, OneOf<EventListenerOptions, bool> options,
            EventListenerActions actions = null)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener, Selector, type, DotNetObjectReference.Create(
                new Invoker<object>(
                    async (p) =>
                    {
                        if (listener.HasDelegate)
                        {
                            await listener.InvokeAsync();
                        }
                    })), options.Value, actions);
        }

        public async Task AddEventListenerAsync<T>(string type, EventCallback<T> listener, OneOf<EventListenerOptions, bool> options,
            EventListenerActions actions = null)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener, Selector, type, DotNetObjectReference.Create(
                new Invoker<T>(
                    async (p) =>
                    {
                        if (listener.HasDelegate)
                        {
                            await listener.InvokeAsync(p);
                        }
                    })), options.Value, actions);
        }

        public async Task RemoveEventListenerAsync(string type)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, Selector, type);
        }

        public HtmlElement QuerySelector(string selector)
        {
            return new HtmlElement(JS, $"{Selector} {selector}");
        }
    }
}