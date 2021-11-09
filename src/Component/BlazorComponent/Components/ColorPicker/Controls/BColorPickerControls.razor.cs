using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BColorPickerControls<TColorPicker> where TColorPicker : IColorPicker
    {
        public bool HideSliders => Component.HideSliders;

        public bool HideInputs => Component.HideInputs;
    }
}
