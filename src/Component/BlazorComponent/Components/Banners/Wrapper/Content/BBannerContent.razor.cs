namespace BlazorComponent
{
    public partial class BBannerContent<TBanner> where TBanner : IBanner
    {
        public bool HasIcon => Component.HasIcon;
    }
}
