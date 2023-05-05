namespace BlazorComponent
{
    public interface IToolbar : ISheet
    {
        string? Src { get; }

        RenderFragment<Dictionary<string, object>>? ImgContent { get; }

        StringNumber? Height { get; }

        bool IsExtended { get; }

        RenderFragment? ExtensionContent { get; }
    }
}