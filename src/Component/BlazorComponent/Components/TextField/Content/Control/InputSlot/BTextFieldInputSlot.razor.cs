using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldInputSlot<TValue>
    {
        protected Dictionary<string, object> InputSlotAttrs => Component.InputSlotAttrs;
    }
}
