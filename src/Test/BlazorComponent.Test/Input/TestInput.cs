using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Test.Input
{
    public class TestInput : BInput<string>
    {
    }

    public abstract class MockInput : IHandleEvent, IInput<string>
    {
        public abstract string Value { get; }

        public abstract RenderFragment ChildContent { get; }

        public abstract ComponentCssProvider CssProvider { get; }

        public abstract ComponentAbstractProvider AbstractProvider { get; }

        public abstract Task HandleOnClickAsync(MouseEventArgs args);

        void IComponent.Attach(RenderHandle renderHandle)
        {
            throw new NotImplementedException();
        }

        Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem item, object arg)
        {
            return item.InvokeAsync(arg);
        }

        Task IHandleAfterRender.OnAfterRenderAsync()
        {
            throw new NotImplementedException();
        }

        Task IComponent.SetParametersAsync(ParameterView parameters)
        {
            throw new NotImplementedException();
        }
    }
}
