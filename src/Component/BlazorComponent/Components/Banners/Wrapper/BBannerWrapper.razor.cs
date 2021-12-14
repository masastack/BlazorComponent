using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBannerWrapper<TBanner> where TBanner : IBanner
    {
        public RenderFragment ComputedActionsContent => Component.ComputedActionsContent;
    }
}
