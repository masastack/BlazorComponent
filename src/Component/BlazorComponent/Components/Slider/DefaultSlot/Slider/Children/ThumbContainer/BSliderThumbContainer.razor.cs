namespace BlazorComponent
{
    public partial class BSliderThumbContainer<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public ElementReference ThumbElement
        {
            set { Component.ThumbElement = value; }
        }

        public Dictionary<string, object> ThumbAttrs => Component.ThumbAttrs;

        public EventCallback<FocusEventArgs> OnFocus => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnFocusAsync);

        public EventCallback<FocusEventArgs> OnBlur => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnBlurAsync);

        public EventCallback<KeyboardEventArgs> OnKeyDown =>
            EventCallback.Factory.Create<KeyboardEventArgs>(Component, Component.HandleOnKeyDownAsync);

        public bool ShowThumbLabel => Component.ShowThumbLabel;
    }
}
