using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BColorPickerSliders<TColorPickerPreview> where TColorPickerPreview : IColorPickerPreview
    {
        public bool HideAlpha => Component.HideAlpha;
    }
}
