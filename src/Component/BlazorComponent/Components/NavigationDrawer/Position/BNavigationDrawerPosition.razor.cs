namespace BlazorComponent
{
    public partial class BNavigationDrawerPosition<TNavigationDrawer> where TNavigationDrawer : INavigationDrawer
    {
        [Parameter]
        public string ClassName { get; set; } = null!;

        public RenderFragment? PositionContent => "prepend".Equals(ClassName) ?
            Component.PrependContent : Component.AppendContent;
    }
}
