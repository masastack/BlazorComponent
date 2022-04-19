using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class TransitionElementBase<TValue> : Element, IAsyncDisposable
    {
        [Inject]
        protected IJSRuntime Js { get; set; }

        [Parameter]
        public TValue Value { get; set; }

        [CascadingParameter]
        public Transition? Transition { get; set; }

        private TValue _preValue;
        private TransitionJsInvoker? _transitionJsInvoker;

        protected bool FirstRender { get; set; } = true;

        protected virtual TransitionState CurrentState { get; }

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
                            await BeforeLeave();
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

            if (_transitionJsInvoker is null)
            {
                if (Reference.Context is null)
                {
                    return;
                }

                _transitionJsInvoker = new TransitionJsInvoker(Js);
                await _transitionJsInvoker.Init(OnTransitionEnd, OnTransitionCancel);
                await InteropCall();
            }
        }

        protected abstract void StartTransition();

        protected async Task RequestAnimationFrameAsync(Func<Task> callback)
        {
            await Task.Delay(16);
            await callback();
        }

        protected async Task InteropCall() // TODO: rename method
        {
            if (Reference.Context is not null && _transitionJsInvoker is not null)
            {
                await _transitionJsInvoker.AddTransitionEvents(Reference);
            }
        }

        protected virtual Task OnTransitionEnd(string referenceId, LeaveEnter transition) => Task.CompletedTask;

        protected virtual Task OnTransitionCancel() => Task.CompletedTask;

        protected virtual Task BeforeLeave() => Task.CompletedTask;

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