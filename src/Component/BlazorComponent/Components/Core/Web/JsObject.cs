using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent.Web
{
    public class JsObject : IJsObject
    {
        protected IJSRuntime JS { get; private set; }

        protected ElementReference ElementReference { get; private set; }

        void IJsObject.Attach(IJSRuntime js, ElementReference elementReference)
        {
            JS = js;
            ElementReference = elementReference;
        }
    }
}
