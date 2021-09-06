using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputSlot<TValue, TInput> : ComponentAbstractBase<TInput>
        where TInput : IInput<TValue>
    {
        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public string Location { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
