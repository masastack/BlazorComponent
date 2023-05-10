namespace BlazorComponent
{
    public partial class BButtonContent<TButton> : ComponentPartBase<TButton>
        where TButton : IButton
    {
        protected RenderFragment? ChildContent => Component.ChildContent;
    }
}