namespace BlazorComponent
{
    public partial class BSliderTrackContainer<TValue, TNumeric, TInput> where TInput : ISlider<TValue, TNumeric>
    {
        public ElementReference TrackElement
        {
            set { Component.TrackElement = value; }
        }
    }
}
