using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class VScrollXReverseTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scroll-x-reverse-transition";
        }
    }
}
