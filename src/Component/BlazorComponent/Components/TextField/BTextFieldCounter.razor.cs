using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldCounter<TValue, TInput> where TInput : ITextField<TValue>
    {
        public bool HasCounter => Component.HasCounter;

        public RenderFragment CounterContent => Component.CounterContent;
    }
}
