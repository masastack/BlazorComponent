using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class BComponentBase : ComponentBase, IDisposable, IHandleEvent
    {
        private readonly Queue<Func<Task>> _nextTickQueue = new();

        [Parameter]
        public ForwardRef RefBack { get; set; } = new ForwardRef();

        [Inject]
        public virtual IJSRuntime Js { get; set; }

        protected bool IsDisposed { get; private set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_nextTickQueue.Count > 0)
            {
                var callbacks = _nextTickQueue.ToArray();
                _nextTickQueue.Clear();

                foreach (var callback in callbacks)
                {
                    if (IsDisposed)
                    {
                        return;
                    }

                    await callback();
                }
            }
        }

        protected void NextTick(Func<Task> callback)
        {
            _nextTickQueue.Enqueue(callback);
        }

        protected void NextTick(Action callback)
        {
            NextTick(() =>
            {
                callback.Invoke();
                return Task.CompletedTask;
            });
        }

        protected async Task NextTickIf(Func<Task> callback, Func<bool> @if)
        {
            if (@if.Invoke())
            {
                NextTick(callback);
            }
            else
            {
                await callback.Invoke();
            }
        }

        protected async Task NextTickWhile(Func<Task> callback, Func<bool> @while, int retryTimes = 5)
        {
            var times = 0;
            while (@while.Invoke() && times < retryTimes)
            {
                times++;
                await Task.Delay(16);
            }

            await callback.Invoke();
        }

        protected void NextTickIf(Action callback, Func<bool> @if)
        {
            if (@if.Invoke())
            {
                NextTick(callback);
            }
            else
            {
                callback.Invoke();
            }
        }

        protected void InvokeStateHasChanged()
        {
            if (!IsDisposed)
            {
                _ = InvokeAsync(StateHasChanged);
            }
        }

        protected async Task InvokeStateHasChangedAsync()
        {
            if (!IsDisposed)
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        protected async Task<T> JsInvokeAsync<T>(string code, params object[] args)
        {
            return await Js.InvokeAsync<T>(code, args);
        }

        protected async Task JsInvokeAsync(string code, params object[] args)
        {
            await Js.InvokeVoidAsync(code, args);
        }

        [CascadingParameter]
        protected IErrorHandler? ErrorHandler { get; set; }

        protected virtual bool AfterHandleEventShouldRender()
        {
            return true;
        }

        Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
        {
            var task = callback.InvokeAsync(arg);
            var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                                  task.Status != TaskStatus.Canceled;

            if (AfterHandleEventShouldRender())
            {
                StateHasChanged();
            }

            return shouldAwaitTask
                ? CallStateHasChangedOnAsyncCompletion(task)
                : Task.CompletedTask;
        }

        private async Task CallStateHasChangedOnAsyncCompletion(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex) // avoiding exception filters for AOT runtime support
            {
                // Ignore exceptions from task cancellations, but don't bother issuing a state change.
                if (task.IsCanceled)
                {
                    return;
                }

                if (ErrorHandler != null)
                {
                    await ErrorHandler.HandleExceptionAsync(ex);
                }
                else
                {
                    throw;
                }
            }

            if (AfterHandleEventShouldRender())
            {
                StateHasChanged();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            IsDisposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        ~BComponentBase()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
    }
}
