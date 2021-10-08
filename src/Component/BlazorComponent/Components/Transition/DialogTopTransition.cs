using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class DialogTopTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "dialog-top-transition";
        }
    }
}
