namespace BlazorComponent.Components.Transition;

public interface ITransitionJSCallback
{
    string? TransitionName { get; }

    bool LeaveAbsolute { get; }

    ElementReference Reference { get; }

    Task HandleOnTransitionend();
}
