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

        public Func<MouseEventArgs, Task> HandleOnClick => Component.HandleOnClick;

        public Func<MouseEventArgs, Task> HandleOnMouseDown => Component.HandleOnMouseDown;

        protected Func<MouseEventArgs, Task> HandleOnMouseUp => Component.HandleOnMouseDown;
    }
}
