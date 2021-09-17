using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BProgressCircularCircle<TProgressCircular> where TProgressCircular : IProgressCircular
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Offset { get; set; }

        public Dictionary<string, object> CircleAttributes => Component.CircleAttributes;
    }
}
