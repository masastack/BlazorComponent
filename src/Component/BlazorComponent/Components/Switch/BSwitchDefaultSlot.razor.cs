using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSwitchDefaultSlot : BInputDefaultSlot<ISwitch>
    {
        public bool IsDisabled => Component.IsDisabled;

        public bool Value => Component.Value;
    }
}
