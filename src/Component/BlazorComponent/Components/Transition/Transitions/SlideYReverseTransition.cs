using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class SlideYReverseTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "slide-y-reverse-transition";
        }
    }
}
