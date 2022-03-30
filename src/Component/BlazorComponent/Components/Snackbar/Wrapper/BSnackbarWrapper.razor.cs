namespace BlazorComponent
{
    public partial class BSnackbarWrapper<TSnackbar> where TSnackbar : ISnackbar
    {
        bool Value => Component.Value;
    }
}
