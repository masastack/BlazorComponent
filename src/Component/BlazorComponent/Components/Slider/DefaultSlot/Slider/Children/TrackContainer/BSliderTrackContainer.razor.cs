using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSliderTrackContainer<TValue, TInput> where TInput : ISlider<TValue>
    {
        public ElementReference TrackElement
        {
            set
            {
                Component.TrackElement = value;
            }
        }
    }
}

