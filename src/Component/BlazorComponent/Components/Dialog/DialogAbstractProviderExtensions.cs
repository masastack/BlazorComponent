namespace BlazorComponent
{
    public static class DialogAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDialogDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDialogActivator<>), typeof(BDialogActivator<IDialog>))
                .Apply(typeof(BDialogContent<>), typeof(BDialogContent<IDialog>))
                .Apply(typeof(BDialogInnerContent<>), typeof(BDialogInnerContent<IDialog>));
        }
    }
}
