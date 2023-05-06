namespace BlazorComponent
{
    public partial class BSnackbarContent<TSnackbar> where TSnackbar : ISnackbar
    {
        public RenderFragment? ComponentChildContent => Component.ChildContent;
    }
}
