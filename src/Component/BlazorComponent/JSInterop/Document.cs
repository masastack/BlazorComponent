using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Web
{
    public class Document : JSObject
    {
        public Document(IJSRuntime js)
            : base(js)

        {
            Selector = "document";
        }

        public HtmlElement GetElementById(string id)
        {
            return new HtmlElement(JS, "#" + id);
        }

        public HtmlElement QuerySelector(string selectors)
        {
            return new HtmlElement(JS, selectors);
        }

        public async Task<T> ExecCommandAsync<T>(string commandId, bool? showUI, object value = null)
        {
            return await JS.InvokeAsync<T>("document.execCommand", commandId, showUI, value);
        }

        public async Task ExecCommandAsync(string commandId, bool? showUI, object value = null)
        {
            await JS.InvokeVoidAsync("document.execCommand", commandId, showUI, value);
        }
    }
}
