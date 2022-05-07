using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BSliderSlider<TValue, TInput> where TInput : ISlider<TValue>
    {
        public EventCallback<MouseEventArgs> HandleOnSliderClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnSliderClickAsync);

        public EventCallback<ExMouseEventArgs> HandleOnSliderMouseDownAsync => EventCallback.Factory.Create<ExMouseEventArgs>(Component, Component.HandleOnSliderMouseDownAsync);

        public EventCallback<ExTouchEventArgs> HandleOnSliderTouchStartAsync => EventCallback.Factory.Create<ExTouchEventArgs>(Component, Component.HandleOnTouchStartAsync);

        public EventCallback<MouseEventArgs> HandleOnSliderMouseUpAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnSliderMouseUpAsync);
    }
}
