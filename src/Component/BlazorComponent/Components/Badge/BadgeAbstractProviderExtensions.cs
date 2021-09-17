namespace BlazorComponent
{
    public static class BadgeAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyBadgeDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BBadgeBadge<>), typeof(BBadgeBadge<IBadge>))
                .Apply(typeof(BBadgeContent<>), typeof(BBadgeContent<IBadge>))
                .Apply(typeof(BBadgeWrapper<>), typeof(BBadgeWrapper<IBadge>));
        }
    }
}
