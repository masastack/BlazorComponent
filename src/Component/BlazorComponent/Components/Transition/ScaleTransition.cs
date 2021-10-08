using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ScaleTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scale-transition";
        }
    }
}
