using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface INavigationDrawer : IHasProviderComponent
    {
        RenderFragment PrependContent { get; }

        RenderFragment ChildContent { get; }

        RenderFragment AppendContent { get; }

        RenderFragment<Dictionary<string, object>> ImgContent { get; }

        string Src { get; }
    }
}
