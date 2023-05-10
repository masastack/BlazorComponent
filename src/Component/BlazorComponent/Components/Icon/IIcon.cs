namespace BlazorComponent
{
    public interface IIcon : IHasProviderComponent, ITransitionIf
    {
        bool Dense { get; set; }

        bool Disabled { get; set; }

        bool Left { get; set; }

        ElementReference Ref { get; set; }

        bool Right { get; set; }

        StringNumber? Size { get; set; }

        string? Tag { get; set; }

        EventCallback<MouseEventArgs> OnClick { get; set; }

        Task HandleOnClick(MouseEventArgs args);
    }
}
