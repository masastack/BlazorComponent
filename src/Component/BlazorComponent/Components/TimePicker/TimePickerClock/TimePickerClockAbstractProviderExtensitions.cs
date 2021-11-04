using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class TimePickerClockAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyTimePickerClocDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                        .Apply(typeof(BTimePickerClockHand<>), typeof(BTimePickerClockHand<ITimePickerClock>))
                        .Apply(typeof(BTimePickerClockValues<>), typeof(BTimePickerClockValues<ITimePickerClock>));
        }
    }
}
