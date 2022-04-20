using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class TransitionElementBase<TValue> : Element, IAsyncDisposable
    {
        [Inject]
        protected IJSRuntime Js { get; set; }

        [CascadingParameter]
        public Transition? Transition { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        private TValue _preValue;
        private TransitionJsInvoker? _transitionJsInvoker;

        protected bool FirstRender { get; private set; } = true;

        protected abstract TransitionState CurrentState { get; }

        internal BlazorComponent.Web.Element? Element { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (!EqualityComparer<TValue>.Default.Equals(Value, _preValue))
            {
                _preValue = Value;

                switch (CurrentState)
                {
                    case TransitionState.None:
                        if (Transition?.Mode is TransitionMode.InOut)
                        {
                            // before enter
                        }
                        else
                        {
                            await BeforeLeaveAsync();
                        }

                        break;
                    case TransitionState.Leave:
                        break;
                    case TransitionState.Enter:
                        break;
                    case TransitionState.EnterTo:
                        break;
                    case TransitionState.LeaveTo:
                        break;
                    case TransitionState.Completed:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                StartTransition();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                FirstRender = false;
            }

            if (Transition is not null)
            {
                if (_transitionJsInvoker is null)
                {
                    if (Reference.Context is null)
                    {
                        return;
                    }

                    _transitionJsInvoker = new TransitionJsInvoker(Js);
                    await _transitionJsInvoker.Init(OnTransitionEndAsync);
                    await RegisterTransitionEventsAsync();
                }

                if (ElementReferenceChanged)
                {
                    ElementReferenceChanged = false;

                    await RegisterTransitionEventsAsync();
                }

                await NextAsync(CurrentState);
            }
        }

        protected abstract void StartTransition();

        /// <summary>
        /// Update to the next transition state.
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        protected abstract Task NextAsync(TransitionState currentState);

        protected virtual Task BeforeLeaveAsync() => Task.CompletedTask;

        protected virtual Task OnTransitionEndAsync(string referenceId, LeaveEnter transition) => Task.CompletedTask;

        protected async Task RequestAnimationFrameAsync(Func<Task> callback)
        {
            await Task.Delay(16);
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
}