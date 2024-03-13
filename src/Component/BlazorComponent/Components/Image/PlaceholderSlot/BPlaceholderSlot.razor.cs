namespace BlazorComponent
{
    public partial class BPlaceholderSlot<TImage> : ComponentPartBase<TImage> where TImage : IImage
    {
        public string? Transition => Component.Transition;

        public bool IsLoading => Component.IsLoading;

        public RenderFragment? PlaceholderContent => Component.PlaceholderContent;
    }
}