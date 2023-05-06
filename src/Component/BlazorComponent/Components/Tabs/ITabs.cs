namespace BlazorComponent
{
    public interface ITabs : IHasProviderComponent
    {
        bool HideSlider { get; }

        string? SliderColor { get; }

        StringNumber SliderSize { get; }

        RenderFragment? ChildContent { get; }

        List<ITabItem> TabItems { get; }

        StringNumber? Value { get; }
    }
}