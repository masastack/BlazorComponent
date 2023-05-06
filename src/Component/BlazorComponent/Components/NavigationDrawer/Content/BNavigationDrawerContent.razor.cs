namespace BlazorComponent
{
    public partial class BNavigationDrawerContent<TNavigationDrawer> where TNavigationDrawer : INavigationDrawer
    {
        public RenderFragment? ComponentChildContent => Component.ChildContent;
    }
}
