using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BOverlayContent<TOverlay> where TOverlay : IOverlay
    {
        public bool Value => Component.Value;

        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
