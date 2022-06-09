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

        public EventCallback<ExMouseEventArgs> HandleOnClickAsync => EventCallback.Factory.Create<ExMouseEventArgs>(Component, Component.HandleOnClickAsync);

        public EventCallback<MouseEventArgs> HandleOnMouseDownAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnMouseDownAsync);

        public EventCallback<ExMouseEventArgs> HandleOnMouseUpAsync => EventCallback.Factory.Create<ExMouseEventArgs>(Component, Component.HandleOnMouseUpAsync);
    }
}
