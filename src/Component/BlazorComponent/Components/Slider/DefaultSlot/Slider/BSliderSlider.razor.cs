namespace BlazorComponent
{
    public partial class BSliderSlider<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public ElementReference SliderElement
        {
            set { Component.SliderElement = value; }
        }
        
        public EventCallback<MouseEventArgs> HandleOnSliderClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnSliderClickAsync);
    }
}
