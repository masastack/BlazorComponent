using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPlaceholderSlot<TImage> : ComponentPartBase<TImage> where TImage : IImage
    {
        public bool IsLoading => Component.IsLoading;
        
        public RenderFragment PlaceholderContent => Component.PlaceholderContent;
    }
}