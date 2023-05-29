namespace BlazorComponent
{
    public partial class BOverlayScrim<TOverlay> where TOverlay : IOverlay
    {
        private bool Scrim => Component.Scrim;
    }
}
