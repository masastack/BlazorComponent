namespace BlazorComponent
{
    public enum TransitionState
    {
        None = 0,
        Enter,
        EnterTo,
        EnterCancelled,
        Leave,
        LeaveTo,
        LeaveCancelled
    }
}
