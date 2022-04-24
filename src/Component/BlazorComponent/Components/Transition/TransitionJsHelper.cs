using Microsoft.JSInterop;

namespace BlazorComponent;

public class TransitionJsHelper
{
    private readonly Func<string, LeaveEnter, Task> _onTransitionEnd;

    public TransitionJsHelper(Func<string, LeaveEnter, Task> onTransitionEnd)
    {
        _onTransitionEnd = onTransitionEnd;
    }

    [JSInvokable]
    public async Task OnTransitionEnd(string referenceId, string transition)
    {
        var leaveOrEnter = transition == "leave" ? LeaveEnter.Leave : LeaveEnter.Enter;

        await _onTransitionEnd.Invoke(referenceId, leaveOrEnter);
    }
}