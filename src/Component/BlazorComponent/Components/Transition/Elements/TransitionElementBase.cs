using System.Text;

namespace BlazorComponent;

public abstract class TransitionElementBase : Element
{
    internal abstract TransitionState CurrentState { get; set; }

    /// <summary>
    /// The dom information about the transitional element.
    /// </summary>
    internal BlazorComponent.Web.Element? ElementInfo { get; set; }
}

#if NET6_0
public abstract class TransitionElementBase<TValue> : TransitionElementBase, IAsyncDisposable
#else
public abstract class TransitionElementBase<TValue> : TransitionElementBase, IAsyncDisposable where TValue : notnull
#endif
{
    [Inject] [NotNull] protected IJSRuntime? Js { get; set; }

    [CascadingParameter] public Transition? Transition { get; set; }

    [Parameter, EditorRequired] public TValue Value { get; set; } = default!;

    private TValue? _preValue;
    private TransitionJsInvoker? _transitionJsInvoker;

    protected bool FirstRender { get; private set; } = true;

    /// <summary>
    /// Whether it is a transitional element.
    /// </summary>
    protected bool HavingTransition =>
        !string.IsNullOrWhiteSpace(Transition?.Name) && Transition?.TransitionElement == this;

    /// <summary>
    /// No transition or is not a transitional element.
    /// </summary>
    protected bool NoTransition => !HavingTransition;

    protected override void OnInitialized()
    {
        if (Transition is not null && Transition.TransitionElement is null)
        {
            Transition.TransitionElement = this;
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        if (NoTransition)
        {
            return;
        }

        if (!EqualityComparer<TValue>.Default.Equals(Value, _preValue))
        {
            _preValue = Value;

            if (!FirstRender)
            {
                bool? boolValue = null;
                if (Value is bool @bool)
                {
                    boolValue = @bool;
                }

                if (Transition!.Mode is TransitionMode.InOut || (boolValue.HasValue && boolValue.Value))
                {
                    await Transition!.BeforeEnter(this);
                }
                else
                {
                    await Transition!.BeforeLeave(this);
                }
            }

            StartTransition();

            await Hooks();
        }
    }

    protected async Task Hooks()
    {
        // hooks
        // TODO: but it hasn't been tested yet

        switch (CurrentState)
        {
            case TransitionState.None:
                break;
            case TransitionState.Enter:
                if (!FirstRender)
                {
                    await Transition!.Enter(this);
                }

                break;
            case TransitionState.EnterTo:
                if (!FirstRender)
                {
                    await Transition!.AfterEnter(this);
                }

                break;
            case TransitionState.EnterCancelled:
                if (!FirstRender)
                {
                    await Transition!.EnterCancelled(this);
                }

                break;
            case TransitionState.Leave:
                if (!FirstRender)
                {
                    await Transition!.Leave(this);
                }

                break;
            case TransitionState.LeaveTo:
                if (!FirstRender)
                {
                    await Transition!.AfterLeave(this);
                }

                break;
            case TransitionState.LeaveCancelled:
                if (!FirstRender)
                {
                    await Transition!.LeaveCancelled(this);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected bool RequestingAnimationFrame;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            FirstRender = false;
        }

        if (HavingTransition)
        {
            if (_transitionJsInvoker?.Registered is not true)
            {
                if (Reference.Context is null)
                {
                    return;
                }

                Transition!.ElementReference = Reference;

                _transitionJsInvoker = new TransitionJsInvoker(Js);
                await _transitionJsInvoker.Init(OnTransitionEndAsync, OnTransitionCancelAsync);
                if (!_transitionJsInvoker.Registered)
                {
                    await RegisterTransitionEventsAsync();
                }
            }

            if (!firstRender && ElementReferenceChanged)
            {
                ElementReferenceChanged = false;

                Transition!.ElementReference = Reference;

                await RegisterTransitionEventsAsync();
            }

            if (_transitionJsInvoker.Registered && CanMoveNext)
            {
                await NextAsync(CurrentState);
            }
        }
    }

    protected virtual bool CanMoveNext => !RequestingAnimationFrame;

    protected bool IsLeaveTransitionState => CurrentState is TransitionState.Leave or TransitionState.LeaveTo;

    protected override string? ComputedStyle
    {
        get
        {
            if (Transition?.LeaveAbsolute is true && IsLeaveTransitionState && ElementInfo is not null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(Style);
                stringBuilder.Append(' ');
                stringBuilder.Append("position:absolute; ");
                stringBuilder.Append($"top:{ElementInfo.OffsetTop}px; ");
                stringBuilder.Append($"left:{ElementInfo.OffsetLeft}px; ");
                stringBuilder.Append($"width:{ElementInfo.OffsetWidth}px; ");
                stringBuilder.Append($"height:{ElementInfo.OffsetHeight}px; ");

                return stringBuilder.ToString().TrimEnd();
            }

            return base.ComputedStyle;
        }
    }

    protected abstract void StartTransition();

    /// <summary>
    /// Update to the next transition state.
    /// </summary>
    /// <param name="currentState"></param>
    /// <returns></returns>
    protected abstract Task NextAsync(TransitionState currentState);

    protected virtual Task OnTransitionEndAsync(string referenceId, LeaveEnter transition) => Task.CompletedTask;

    protected virtual Task OnTransitionCancelAsync(string referenceId, LeaveEnter transition) => Task.CompletedTask;

    protected async Task RequestAnimationFrameAsync(Func<Task> callback)
    {
        RequestingAnimationFrame = true;
        await Task.Delay(16);
        RequestingAnimationFrame = false;
        await callback();
    }

    private async Task RegisterTransitionEventsAsync()
    {
        if (Reference.Context is not null && _transitionJsInvoker is not null)
        {
            await _transitionJsInvoker.RegisterTransitionEvents(Reference);
        }
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_transitionJsInvoker is not null)
            {
                await _transitionJsInvoker.DisposeAsync();
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
}