namespace BlazorComponent
{
    public partial class BAlertWrapper<TAlert> : ComponentAbstractBase<TAlert> where TAlert : IAlert
    {
        private bool IsShowIcon => Component.IsShowIcon;

        private Borders Border => Component.Border;

        private bool Dismissible => Component.Dismissible;
    }
}