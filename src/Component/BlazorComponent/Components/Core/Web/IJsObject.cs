using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent.Web
{
    public interface IJsObject
    {
        void Attach(IJSRuntime js, ElementReference elementReference);
    }
}
