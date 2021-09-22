using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderDefaultSlot<TInput> where TInput : ISlider
    {
        public bool InverseLabel => Component.InverseLabel;
    }
}
