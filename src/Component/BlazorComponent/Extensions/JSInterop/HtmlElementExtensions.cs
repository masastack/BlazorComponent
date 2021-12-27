using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BlazorComponent.Web
{
    public static class HtmlElementExtensions
    {
        /// <summary>
        /// We will remove this when transition been refactored
        /// </summary>
        /// <param name="htmlElement"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        internal static async Task<double?> GetSizeAsync(this HtmlElement htmlElement, string prop)
        {
            var jsonElement = await htmlElement.JS.InvokeAsync<JsonElement>(JsInteropConstants.GetSize, htmlElement.Selector, prop);
            return jsonElement.ValueKind == JsonValueKind.Number ? jsonElement.GetDouble() : null;
        }
    }
}
