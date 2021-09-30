namespace BlazorComponent
{
    public static class TimelineItemAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyTimelineItemDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BTimelineItemBody<>), typeof(BTimelineItemBody<ITimelineItem>))
                .Apply(typeof(BTimelineItemDivider<>), typeof(BTimelineItemDivider<ITimelineItem>))
                .Apply(typeof(BTimelineItemDot<>), typeof(BTimelineItemDot<ITimelineItem>))
                .Apply(typeof(BTimelineItemInnerDot<>), typeof(BTimelineItemInnerDot<ITimelineItem>))
                .Apply(typeof(BTimelineItemIcon<>), typeof(BTimelineItemIcon<ITimelineItem>))
                .Apply(typeof(BTimelineItemOpposite<>), typeof(BTimelineItemOpposite<ITimelineItem>));
        }
    }
}
