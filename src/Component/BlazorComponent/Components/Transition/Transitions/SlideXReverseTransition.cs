using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class SlideXReverseTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "slide-x-reverse-transition";
        }
    }
}
