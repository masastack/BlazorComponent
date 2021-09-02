using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BAlertDismissButton<TAlert> : ComponentAbstractBase<TAlert>
        where TAlert : IAlert
    {
        private string CloseIcon => Component.CloseIcon;

        private string CloseLabel => Component.CloseLabel;

        private EventCallback<MouseEventArgs> HandleOnDismiss =>
            EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnDismiss);
    }
}