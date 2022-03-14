using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ShowTransitionElement : ToggleableTransitionElement
    {
        protected override string ComputedStyle
        {
            get
            {
                if (!LazyValue)
                {
                    return string.Join(";", base.ComputedStyle, "display:none");
                }

                return base.ComputedStyle;
            }
        }
    }
}
