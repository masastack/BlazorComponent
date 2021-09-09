using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent.Web
{
    public class JsObjectFactory
    {
        private IJSRuntime _js;

        public JsObjectFactory(IJSRuntime js)
        {
            _js = js;
        }

        public TObject CreateObject<TObject>(ElementReference elementReference)
            where TObject : IJsObject, new()
        {
            var obj = new TObject();
            obj.Attach(_js, elementReference);

            return obj;
        }
    }
}
