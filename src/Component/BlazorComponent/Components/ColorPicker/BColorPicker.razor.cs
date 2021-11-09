using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BColorPicker
    {
        [Parameter]
        public bool HideCanvas { get; set; }

        [Parameter]
        public bool HideSliders { get; set; }

        [Parameter]
        public bool HideInputs { get; set; }

        [Parameter]
        public bool HideModeSwitch { get; set; }

        [Parameter]
        public bool ShowSwatches { get; set; }
    }
}
