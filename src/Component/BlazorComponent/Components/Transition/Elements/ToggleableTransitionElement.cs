namespace BlazorComponent;

public abstract class ToggleableTransitionElement : TransitionElementBase<bool>
{
    [Parameter(CaptureUnmatchedValues = true)]
    public override IDictionary<string, object> AdditionalAttributes
    {
        get
        {
            var attributes = base.AdditionalAttributes ?? new Dictionary<string, object>();

            attributes["class"] = ComputedClass;
            attributes["style"] = ComputedStyle;

            return attributes;
        }
        set => base.AdditionalAttributes = value;
    }

    private TransitionState State { get;  set; }

    protected bool LazyValue { get; private set; }

    protected override string ComputedClass
    {
        get
        {
            if (Transition == null) return Class;

            var transitionClass = Transition.GetClass(State);
            return string.Join(" ", Class, transitionClass);
        }
    }

    protected override string ComputedStyle
    {
        get
        {
            if (Transition == null) return Style;

            var transitionStyle = Transition.GetStyle(State);
            return string.Join(';', Style, transitionStyle);
        }
    }

    internal override TransitionState CurrentState
    {
        get => State;
        set => State = value;
    }

    protected override void OnParametersSet()
    {
        if (NoTransition)
        {
            if (Value)
            {
                ShowElement();
            }
            else
            {
                HideElement();
            }
        }
    }

    protected override void StartTransition()
    {
        //Don't trigger transition in first render
        if (FirstRender)
        {
            ShowElement();
            return;
        }

        if (Value)
        {
            ShowElement();
            State = TransitionState.Enter;
        }
        else
        {
            State = TransitionState.Leave;
        }
    }

    protected override async Task NextAsync(TransitionState state)
    {
        switch (state)
        {
            case TransitionState.Enter:
                await RequestNextStateAsync(TransitionState.EnterTo);
                break;
            case TransitionState.Leave:
                await RequestNextStateAsync(TransitionState.LeaveTo);
                break;
        }
    }

    protected override Task OnTransitionEndAsync(string referenceId, LeaveEnter transition)
    {
        if (referenceId != Reference.Id)
        {
            return Task.CompletedTask;
        }

        Console.WriteLine($"{referenceId}: END!!! {transition}");

        if (transition == LeaveEnter.Enter && CurrentState == TransitionState.EnterTo)
        {
            NextState(TransitionState.None);
        }
        else if (transition == LeaveEnter.Leave && CurrentState == TransitionState.LeaveTo)
        {
            HideElement();
            NextState(TransitionState.None);
        }

        return Task.CompletedTask;
    }

    protected override Task OnTransitionCancelAsync(string referenceId, LeaveEnter transition)
    {
        if (referenceId != Reference.Id)
        {
            return Task.CompletedTask;
        }

        // Console.WriteLine($"{referenceId}: CANCEL!!! {transition}");

        // if (transition == LeaveEnter.Enter && CurrentState == TransitionState.EnterTo)
        // {
        //     NextState(TransitionState.EnterCancelled);
        // }
        // else if (transition == LeaveEnter.Leave && CurrentState == TransitionState.LeaveTo)
        // {
        //     NextState(TransitionState.LeaveCancelled);
        // }

        return Task.CompletedTask;
    }

    private void NextState(TransitionState transitionState)
    {
        State = transitionState;
        // StateHasChanged();
        Transition?.InvokeStateHasChanged();
    }

    private async Task RequestNextStateAsync(TransitionState state)
    {
        await RequestAnimationFrameAsync(() =>
        {
            NextState(state);
            return Task.CompletedTask;
        });
    }

    private void HideElement()
    {
        LazyValue = false;
    }

    private void ShowElement()
    {
        LazyValue = true;
    }
}
