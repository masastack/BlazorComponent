namespace BlazorComponent
{
    public partial class BAlertContent<TAlert> : ComponentPartBase<TAlert>
        where TAlert : IAlert
    {
        protected string? Title => Component.Title;

        protected RenderFragment? TitleContent => Component.TitleContent;

        protected RenderFragment? ChildContent => Component.ChildContent;

        private bool HasTitle => !string.IsNullOrWhiteSpace(Title) || TitleContent is not null;
    }
}
