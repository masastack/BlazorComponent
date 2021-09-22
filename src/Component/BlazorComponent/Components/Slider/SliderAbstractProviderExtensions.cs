using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class SliderAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplySliderDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                    .ApplyInputDefault<double>()
                    .Merge(typeof(BInputDefaultSlot<,>), typeof(BSliderDefaultSlot<ISlider>))
                    .Apply(typeof(BSliderSlider<>),typeof(BSliderSlider<ISlider>))
                    .Apply(typeof(BSliderChildren<>),typeof(BSliderChildren<ISlider>))
                    .Apply(typeof(BSliderInput<>),typeof(BSliderInput<ISlider>))
                    .Apply(typeof(BSliderSteps<>),typeof(BSliderSteps<ISlider>))
                    .Apply(typeof(BSliderThumbContainer<>),typeof(BSliderThumbContainer<ISlider>))
                    .Apply(typeof(BSliderThumb<>),typeof(BSliderThumb<ISlider>))
                    .Apply(typeof(BSliderThumbLabel<>),typeof(BSliderThumbLabel<ISlider>))
                    .Apply(typeof(BSliderTrackContainer<>),typeof(BSliderTrackContainer<ISlider>));
        }
    }
}
