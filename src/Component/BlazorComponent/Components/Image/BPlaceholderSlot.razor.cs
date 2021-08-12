using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPlaceholderSlot<TImage> : ComponentAbstractBase<TImage> where TImage : IImage
    {
        public RenderFragment ChildContent => Component.ChildContent;
    }
}