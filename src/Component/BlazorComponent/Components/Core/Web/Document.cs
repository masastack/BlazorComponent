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

        public HtmlElement QuerySelector(string selectors)
        {
            return new HtmlElement(_js, selectors);
        }

        public HtmlElement QuerySelector(ElementReference elementReference)
        {
            return new HtmlElement(_js, $"[_bl_{elementReference.Id}]");
        }
    }
}
