namespace BlazorComponent;

public partial class BImageContent<TImage> : ComponentPartBase<TImage> where TImage : IImage
{
    public string? Src => Component.Src;

    public string? LazySrc => Component.LazySrc;

    public string? Gradient => Component.Gradient;
}