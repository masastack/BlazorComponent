using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTimelineItemIcon<TTimelineItem> where TTimelineItem : ITimelineItem
    {
        public bool HasIcon => !string.IsNullOrWhiteSpace(Component.Icon) || Component.IconContent != null;

        public RenderFragment ComponentIconContent => Component.IconContent;

        public string Icon => Component.Icon;
    }
}
