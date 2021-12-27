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
