namespace BlazorComponent
{
    public static class NavigationDrawerAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyNavigationDrawerDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BNavigationDrawerBackground<>), typeof(BNavigationDrawerBackground<INavigationDrawer>))
                .Apply(typeof(BNavigationDrawerBorder<>), typeof(BNavigationDrawerBorder<INavigationDrawer>))
                .Apply(typeof(BNavigationDrawerContent<>), typeof(BNavigationDrawerContent<INavigationDrawer>))
                .Apply(typeof(BNavigationDrawerPosition<>), typeof(BNavigationDrawerPosition<INavigationDrawer>));
        }
    }
}
