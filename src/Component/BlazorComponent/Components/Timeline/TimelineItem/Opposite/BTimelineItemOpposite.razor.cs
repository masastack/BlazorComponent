using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTimelineItemOpposite<TTimelineItem> where TTimelineItem : ITimelineItem
    {
        public RenderFragment OppositeContent => Component.OppositeContent;
    }
}
