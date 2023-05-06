namespace BlazorComponent
{
    public partial class BTimelineItemBody<TTimelineItem> where TTimelineItem : ITimelineItem
    {
        public RenderFragment? ComponentChildContent => Component.ChildContent;
    }
}
