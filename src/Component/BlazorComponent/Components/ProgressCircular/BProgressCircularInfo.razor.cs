using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BProgressCircularInfo<TProgressCircular> where TProgressCircular : IProgressCircular
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
