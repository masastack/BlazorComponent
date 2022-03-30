using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BNavigationDrawerPosition<TNavigationDrawer> where TNavigationDrawer : INavigationDrawer
    {
        [Parameter]
        public string ClassName { get; set; }

        public RenderFragment PositionContent => "prepend".Equals(ClassName) ?
            Component.PrependContent : Component.AppendContent;
    }
}
