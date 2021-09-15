namespace BlazorComponent
{
    public static class BannerAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyBannerDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BBannerActions<>), typeof(BBannerActions<IBanner>))
                .Apply(typeof(BBannerContent<>), typeof(BBannerContent<IBanner>))
                .Apply(typeof(BBannerIcon<>), typeof(BBannerIcon<IBanner>))
                .Apply(typeof(BBannerText<>), typeof(BBannerText<IBanner>))
                .Apply(typeof(BBannerWrapper<>), typeof(BBannerWrapper<IBanner>));
        }
    }
}
