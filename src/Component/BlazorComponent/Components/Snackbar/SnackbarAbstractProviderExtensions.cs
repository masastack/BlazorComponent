namespace BlazorComponent
{
    public static class SnackbarAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplySnackbarDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BSnackbarAction<>), typeof(BSnackbarAction<ISnackbar>))
                .Apply(typeof(BSnackbarContent<>), typeof(BSnackbarContent<ISnackbar>))
                .Apply(typeof(BSnackbarWrapper<>), typeof(BSnackbarWrapper<ISnackbar>));
        }
    }
}
