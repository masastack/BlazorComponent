using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BContainer
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
