namespace BlazorComponent
{
    public interface ITabs : IHasProviderComponent
    {
        bool HideSlider { get; }

        string SliderColor { get; }

        StringNumber SliderSize { get; }
    }
}