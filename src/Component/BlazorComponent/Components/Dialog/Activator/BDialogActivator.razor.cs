namespace BlazorComponent
{
    public partial class BDialogActivator<TDialog> where TDialog : IDialog
    {
        public RenderFragment? ComputedActivatorContent => Component.ComputedActivatorContent;
    }
}
