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
        private bool _transitionRunning;

        protected bool FirstRender { get; private set; } = true;

        protected abstract TransitionState CurrentState { get; }

        internal BlazorComponent.Web.Element? Element { get; set; }

        protected override void OnInitialized()
        {
            Console.WriteLine($"{Reference.Id} OnInitialized...");
        }

        protected override async Task OnParametersSetAsync()
        {
            Console.WriteLine($"{Reference.Id} OnParametersSetAsync FirstRender:{FirstRender}");
            if (!EqualityComparer<TValue>.Default.Equals(Value, _preValue))
            {
                _preValue = Value;

                StartTransition();
                _transitionRunning = true;
            }

            if (_transitionRunning)
            {
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
                        Console.WriteLine($"OnParametersSet: {CurrentState}");
                        if (Transition is not null)
                        {
                            await Transition.EnterAsync(Reference);
                        }

                        break;
                    case TransitionState.EnterTo:
                        if (Value is true || Transition?.Mode is TransitionMode.OutIn)
                        {
                            _transitionRunning = false;
                        }
                        break;
                    case TransitionState.LeaveTo:
                        if (Value is false || Transition?.Mode is TransitionMode.InOut)
                        {
                            _transitionRunning = false;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine($"{Reference.Id} firstRender:{firstRender}");

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