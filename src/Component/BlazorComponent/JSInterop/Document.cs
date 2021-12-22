using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Web
{
    public class Document
    {
        private IJSRuntime _js;

        public Document(IJSRuntime js)
        {
            _js = js;
        }

        internal IJSRuntime JS => _js;

        public HtmlElement GetElementById(string id)
        {
            return new HtmlElement(_js, "#" + id);
        }

        public HtmlElement QuerySelector(string selectors)
        {
            return new HtmlElement(_js, selectors);
        }

        public async Task<T> ExecCommandAsync<T>(string commandId, bool? showUI, object value = null)
        {
            return await _js.InvokeAsync<T>("document.execCommand", commandId, showUI, value);
        }

        public async Task ExecCommandAsync(string commandId, bool? showUI, object value = null)
        {
            await _js.InvokeVoidAsync("document.execCommand", commandId, showUI, value);
        }
    }
}
