using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public class EmptyComponent : ComponentBase
    {
        //Avoid exception
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? Attrs { get; set; }
    }
}
