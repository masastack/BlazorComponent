using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputInputSlot<TInput> : ComponentAbstractBase<TInput>
          where TInput : IInput
    {
        //TODO:refs will change in feature
        public ElementReference InputSlotRef
        {
            get
            {
                return Component.InputSlotRef;
            }
            set
            {
                Component.InputSlotRef = value;
            }
        }

        public EventCallback<MouseEventArgs> HandleOnClick => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnClick);

        public EventCallback<MouseEventArgs> HandleOnMouseDown => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnMouseDown);

        protected EventCallback<MouseEventArgs> HandleOnMouseUp => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnMouseDown);
    }
}
