using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSwitchSwitch<TInput> where TInput : ISwitch
    {
        string LeftText => Component.LeftText;

        string RightText => Component.RightText;
    }
}
