namespace BlazorComponent
{
    public interface IImage : IResponsive
    {
        RenderFragment? PlaceholderContent { get; }

        bool IsLoading { get; }

        bool Contain { get; }

        string? LazySrc { get; }

        string? Src { get; }

        string? Gradient { get; }

        string? Transition { get; }
    }
}