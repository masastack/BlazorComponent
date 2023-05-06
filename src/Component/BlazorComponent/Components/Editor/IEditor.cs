namespace BlazorComponent
{
    public interface IEditor
    {
        RenderFragment? ToolbarContent { get; }
        string? Placeholder { get; }
        Task<string?> GetTextAsync();
        Task<string?> GetHtmlAsync();
        Task<string?> GetContentAsync();
        Task SetHtmlAsync();
        Task SetContentAsync();
    }
}
