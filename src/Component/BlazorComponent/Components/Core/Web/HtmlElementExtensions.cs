using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorComponent.Web
{
    public static class HtmlElementExtensions
    {
        public static async Task<double> GetSizeAsync(this HtmlElement htmlElement, string prop)
        {
            var size = await htmlElement.JS.InvokeAsync<double>(JsInteropConstants.GetSize, htmlElement.Selectors, prop);
            return size;
        }
    }
}
