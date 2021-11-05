using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class RangeSliderAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyRangeSliderDefault<TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BSliderThumbContainer<,>), typeof(BRangeSliderThumbContainer<TValue, IRangeSlider<TValue>>))
                .Merge(typeof(BSliderInput<,>), typeof(BRangeSliderInput<TValue, IRangeSlider<TValue>>))
                .Merge(typeof(BSliderTrackContainer<,>), typeof(BRangeSliderTrackContainer<TValue, IRangeSlider<TValue>>));
        }
    }
}
