using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent.Web
{
    public static class HtmlElementExtensions
    {
        public static async Task<double> GetSizeAsync(this HtmlElement htmlElement, string prop)
        {
            var size = await htmlElement.JS.InvokeAsync<double>(JsInteropConstants.GetSize, htmlElement.Selectors, prop);
            return size;
        }

        public static async Task<TProp> GetPropAsync<TProp>(this HtmlElement htmlElement, string name)
        {
            var prop = await htmlElement.JS.InvokeAsync<TProp>(JsInteropConstants.GetProp, htmlElement.Selectors, name);
            return prop;
        }

        public static async Task<double> GetScrollHeightWithoutHeight(this HtmlElement htmlElement)
        {
            var scrollHeight = await htmlElement.JS.InvokeAsync<double>(JsInteropConstants.ScrollHeightWithoutHeight, htmlElement.Selectors);
            return scrollHeight;
        }

        public static async Task UpdateWindowTransitionAsync(this HtmlElement htmlElement, bool isActive, ElementReference? item = null)
        {
            await htmlElement.JS.InvokeVoidAsync(JsInteropConstants.UpdateWindowTransition, htmlElement.Selectors, isActive, item);
        }
    }
}
