using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class TimePickerAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyTimePickerDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BTimePickerPickerBody<>), typeof(BTimePickerPickerBody<ITimePicker>))
                .Apply(typeof(BTimePickerClockAmPm<>), typeof(BTimePickerClockAmPm<ITimePicker>))
                .Apply(typeof(BPickerTitlePickerButton<>), typeof(BPickerTitlePickerButton<ITimePicker>));
        }
    }
}
