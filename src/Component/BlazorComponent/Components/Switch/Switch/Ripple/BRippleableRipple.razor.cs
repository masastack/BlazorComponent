using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BRippleableRipple<TComponent> where TComponent : IRippleable
    {
        public bool Ripple => Component.Ripple ?? true;
    }
}
