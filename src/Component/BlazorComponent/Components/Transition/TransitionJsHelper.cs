namespace BlazorComponent;

public class TransitionJsHelper
{
    private readonly Func<string, LeaveEnter, Task> _onTransitionEnd;
    private readonly Func<string, LeaveEnter, Task> _onTransitionCancel;

    public TransitionJsHelper(Func<string, LeaveEnter, Task> onTransitionEnd, Func<string, LeaveEnter, Task> onTransitionCancel)
    {
        _onTransitionEnd = onTransitionEnd;
        _onTransitionCancel = onTransitionCancel;
    }

    [JSInvokable]
    public async Task OnTransitionEnd(string referenceId, string transition)
    {
        var leaveOrEnter = transition == "leave" ? LeaveEnter.Leave : LeaveEnter.Enter;

        await _onTransitionEnd.Invoke(referenceId, leaveOrEnter);
    }

    [JSInvokable]
    public async Task OnTransitionCancel(string referenceId, string transition)
    {
        var leaveOrEnter = transition == "leave" ? LeaveEnter.Leave : LeaveEnter.Enter;

        await _onTransitionCancel.Invoke(referenceId, leaveOrEnter);
    }
}
