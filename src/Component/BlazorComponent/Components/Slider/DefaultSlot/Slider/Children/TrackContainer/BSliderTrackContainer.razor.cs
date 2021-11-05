using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderTrackContainer<TValue,TInput> where TInput : ISlider<TValue>
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

