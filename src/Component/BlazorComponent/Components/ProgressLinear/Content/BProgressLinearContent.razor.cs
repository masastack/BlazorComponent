using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BProgressLinearContent<TProgressLinear> where TProgressLinear : IProgressLinear
    {
        public double Value => Component.Value;

        public RenderFragment<double> ComponentChildContent => Component.ChildContent;
    }
}
