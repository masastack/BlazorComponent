using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorComponent.Web
{
    public static class WindowExtensions
    {
        public static ValueTask<TProp> GetPropAsync<TProp>(this Window window, string name)
        {
            return window.JS.InvokeAsync<TProp>(JsInteropConstants.GetProp, "window", name);
        }
    }
}
