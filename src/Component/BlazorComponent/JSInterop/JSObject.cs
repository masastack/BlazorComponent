using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Web
{
    public abstract class JSObject
    {
        public JSObject()
        {
        }

        public JSObject(IJSRuntime js)
        {
            JS = js;
        }

        public IJSRuntime JS { get; }

        public string Selector { get; protected set; }
    }
}
