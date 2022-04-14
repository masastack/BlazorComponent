using Microsoft.JSInterop;

namespace BlazorComponent;

public class TransitionJsHelper
{
    private readonly Func<string, LeaveEnter, Task> _onTransitionEnd;
    private readonly Func<Task> _onTransitionCancel;

    public TransitionJsHelper(Func<string, LeaveEnter, Task> onTransitionEnd, Func<Task> onTransitionCancel)
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
    public async Task OnTransitionCancel()
    {
        await _onTransitionCancel.Invoke();
    }
}