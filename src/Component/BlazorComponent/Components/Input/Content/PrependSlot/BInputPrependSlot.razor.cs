using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BInputPrependSlot<TValue, TInput> : ComponentPartBase<TInput>
        where TInput : IInput<TValue>
    {
        public RenderFragment PrependContent => Component.PrependContent;

        public string PrependIcon => Component.PrependIcon;

        public EventCallback<MouseEventArgs> HandleOnPrependClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnPrependClickAsync);
    }
}
