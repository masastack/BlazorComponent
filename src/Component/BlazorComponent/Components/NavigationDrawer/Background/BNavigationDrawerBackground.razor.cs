namespace BlazorComponent
{
    public partial class BNavigationDrawerBackground<TNavigationDrawer> where TNavigationDrawer : INavigationDrawer
    {
        public RenderFragment<Dictionary<string, object?>>? ImgContent => Component.ImgContent;

        public Dictionary<string, object?> ImgProps => new()
        {
            { "Height", "100%" },
            { "Width", "100%" },
            { "Src", Component.Src }
        };
    }
}
