namespace BlazorComponent
{
    public interface IMarkdown : IHasProviderComponent
    {
        string? Value { get; }
        string? Html { get; }
    }
}