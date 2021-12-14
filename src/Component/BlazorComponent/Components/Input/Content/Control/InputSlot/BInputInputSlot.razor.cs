using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputInputSlot<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        public ElementReference InputSlotRef
        {
            get
            {
                return Component.InputSlotElement;
            }
            set
            {
                Component.InputSlotElement = value;
            }
        }

        public EventCallback<MouseEventArgs> HandleOnClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnClickAsync);

        public EventCallback<MouseEventArgs> HandleOnMouseDownAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnMouseDownAsync);

        public EventCallback<MouseEventArgs> HandleOnMouseUpAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnMouseUpAsync);
    }
}
