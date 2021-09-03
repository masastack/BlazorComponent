namespace BlazorComponent
{
    public partial class BAlertWrapper<TAlert> : ComponentAbstractBase<TAlert> where TAlert : IAlert
    {
        protected bool IsShowIcon => Component.IsShowIcon;

        protected Borders Border => Component.Border;

        protected bool Dismissible => Component.Dismissible;
    }
}