namespace BlazorComponent
{
    public partial class BBannerIcon<TBanner> where TBanner : IBanner
    {
        public RenderFragment? IconContent => Component.IconContent;

        public bool HasIcon => Component.HasIcon;

        public string? Icon => Component.Icon;
    }
}
