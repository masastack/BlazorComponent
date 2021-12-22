using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent.Web
{
    public static class DocumentExtensions
    {
        public static ValueTask<TProp> GetPropAsync<TProp>(this Document document, string name)
        {
            return document.JS.InvokeAsync<TProp>(JsInteropConstants.GetProp, "document", name);
        }

        public static HtmlElement GetElementByReference(this Document document, ElementReference elementReference)
        {
            if (elementReference.Id == null)
            {
                return null;
            }

            return new HtmlElement(document.JS, $"[_bl_{elementReference.Id}]");
        }
    }
}
