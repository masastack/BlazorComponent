namespace BlazorComponent
{
    public partial class BSliderSlider<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public EventCallback<MouseEventArgs> HandleOnSliderClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnSliderClickAsync);

        public EventCallback<ExMouseEventArgs> HandleOnSliderMouseDownAsync => EventCallback.Factory.Create<ExMouseEventArgs>(Component, Component.HandleOnSliderMouseDownAsync);

        public EventCallback<ExTouchEventArgs> HandleOnSliderTouchStartAsync => EventCallback.Factory.Create<ExTouchEventArgs>(Component, Component.HandleOnTouchStartAsync);

        public EventCallback<MouseEventArgs> HandleOnSliderMouseUpAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnSliderMouseUpAsync);
    }
}
