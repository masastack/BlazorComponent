using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSwitchSlotBody : BInputSlotBody<ISwitch>
    {
        public ComponentCssProvider CssProvider => Input.CssProvider;

        public bool IsDisabled => Input.IsDisabled;

        public bool Value => Input.Value;
    }
}
