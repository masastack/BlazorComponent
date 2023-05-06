namespace BlazorComponent
{
    public partial class BAlertDismissButton<TAlert> : ComponentPartBase<TAlert>
        where TAlert : IAlert
    {
        protected string CloseIcon => Component.CloseIcon;

        protected string CloseLabel => Component.CloseLabel;

        protected EventCallback<MouseEventArgs> HandleOnDismiss =>
            EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnDismiss);
    }
}