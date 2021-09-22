using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderInput<TInput> where TInput : ISlider
    {
        public double InternalValue => Component.InternalValue;

        public Dictionary<string, object> InputAttrs => Component.InputAttrs;
    }
}

