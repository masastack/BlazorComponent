namespace BlazorComponent
{
    public interface IAlert : IHasProviderComponent
    {
        RenderFragment? IconContent { get; }

        bool IsShowIcon { get; }

        Borders Border { get; }

        RenderFragment? ChildContent { get; }

        string CloseIcon { get; }

        string CloseLabel { get; }

        string? Color { get; }

        bool Dismissible { get; }

        string Tag { get; }

        string? Title { get; }

        RenderFragment? TitleContent { get; }

        AlertTypes Type { get; }

        bool Value { get; }

        EventCallback<bool> ValueChanged { get; }

        Task HandleOnDismiss(MouseEventArgs args);
    }
}
