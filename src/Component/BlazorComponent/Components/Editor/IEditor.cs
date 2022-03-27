using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IEditor : IHasProviderComponent
    {
        RenderFragment EditorContent { get; }
        RenderFragment ToolbarContent { get; }
        string Placeholder { get; }
        Task<string> GetTextAsync();
        Task<string> GetHtmlAsync();
        Task<string> GetContentAsync();
        Task SetHtmlAsync();
        Task SetContentAsync();
    }
}
