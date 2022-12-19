using Microsoft.AspNetCore.Components.Web;

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

        public EventCallback<MouseEventArgs> HandleOnMouseDownAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnMouseDownAsync);
    }
}
