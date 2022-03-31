namespace BlazorComponent
{
    public static class TabsAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyTabsDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BTabsSlider<>), typeof(BTabsSlider<ITabs>))
                .Apply(typeof(BTabsTab<>), typeof(BTabsTab<ITabs>))
                .Apply(typeof(BTabsBody<>), typeof(BTabsBody<ITabs>));
        }
    }
}
