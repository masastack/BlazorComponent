using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BBannerText<TBanner> where TBanner : IBanner
    {
        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}
