namespace BlazorComponent
{
    public partial class BPickerTitlePickerButton<TComponent> where TComponent : IHasProviderComponent
    {
        [Parameter]
        public string Name { get; set; } = null!;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
