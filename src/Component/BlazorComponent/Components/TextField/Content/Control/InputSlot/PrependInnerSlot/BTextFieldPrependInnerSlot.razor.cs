using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldPrependInnerSlot<TValue, TInput> where TInput : ITextField<TValue>
    {
        public RenderFragment PrependInnerContent => Component.PrependInnerContent;

        public string PrependInnerIcon => Component.PrependInnerIcon;

        public EventCallback<MouseEventArgs> HandleOnPrependInnerClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnPrependInnerClickAsync);

        public Action<ElementReference> PrependInnerReferenceCapture => element => Component.PrependInnerElement = element;
    }
}
