using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BlazorComponent
{
    public class TransitionContext
    {
        public string Class => CssBuilder.Class;
        public string Style => StyleBuilder.Style;
        public CssBuilder CssBuilder { get; set; } = new();
        public StyleBuilder StyleBuilder { get; set; } = new();
    }
}
