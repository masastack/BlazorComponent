using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputSlot<TInput> where TInput : IInput
    {
        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public string Location { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
