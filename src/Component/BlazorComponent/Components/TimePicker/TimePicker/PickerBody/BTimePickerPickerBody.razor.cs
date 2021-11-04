using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTimePickerPickerBody<TTimePicker> where TTimePicker : ITimePicker
    {
        public SelectingTimes Selecting => Component.Selecting;

        public bool AmPmInTitle => Component.AmPmInTitle;

        public bool IsAmPm=>Component.IsAmPm;
    }
}
