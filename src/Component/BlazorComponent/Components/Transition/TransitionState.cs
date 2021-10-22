using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public enum TransitionState
    {
        None = 0,
        Enter,
        EnterTo,
        Leave,
        LeaveTo,
        BeforeEnter,
        BeforeLeave
    }
}
