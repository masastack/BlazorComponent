namespace BlazorComponent
{
    public partial class BBannerActions<TBanner> where TBanner : IBanner
    {
        public RenderFragment? ComputedActionsContent => Component.ComputedActionsContent;
    }
}
