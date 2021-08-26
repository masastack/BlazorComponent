using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class EmptyComponent : ComponentBase
    {
        //Avoid exception
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attrs { get; set; }
    }
}
