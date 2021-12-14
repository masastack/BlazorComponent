namespace BlazorComponent
{
    public partial class BAlertWrapper<TAlert> : ComponentPartBase<TAlert> where TAlert : IAlert
    {
        protected bool IsShowIcon => Component.IsShowIcon;

        protected Borders Border => Component.Border;

        protected bool Dismissible => Component.Dismissible;
    }
}