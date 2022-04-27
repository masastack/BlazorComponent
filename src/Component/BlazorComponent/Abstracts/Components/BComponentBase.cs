using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class BComponentBase : ComponentBase, IHandleEvent, IDisposable
    {
        private readonly Queue<Func<Task>> _nextTickQueue = new();

        [Parameter]
        public ForwardRef RefBack { get; set; } = new ForwardRef();

        [Inject]
        public virtual IJSRuntime Js { get; set; }

        [CascadingParameter]
        public IErrorHandler? ErrorHandler { get; set; }

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

        protected bool IsRender { get; set; } = true;

        Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
        {
            var task = callback.InvokeAsync(arg);
            var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                task.Status != TaskStatus.Canceled;

            // After each event, we synchronously re-render (unless !ShouldRender())
            // This just saves the developer the trouble of putting "StateHasChanged();"
            // at the end of every event callback.
            if (IsRender)
            {
                if (AfterHandleEventShouldRender())
                    StateHasChanged();
            }
            else
            {
                IsRender = true;
            }

            return shouldAwaitTask ?
                CallStateHasChangedOnAsyncCompletion(task) :
                Task.CompletedTask;
        }

        private async Task CallStateHasChangedOnAsyncCompletion(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception exception) // avoiding exception filters for AOT runtime support
            {
                if (task.IsCanceled)
                {
                    return;
                }

                if (ErrorHandler != null)
                {
                    await ErrorHandler.HandleExceptionAsync(exception);
                    //IsRender = false;
                }
                else
                {
                    throw;
                }
            }

            if (IsRender)
            {
                if (AfterHandleEventShouldRender())
                    StateHasChanged();
            }
            else
            {
                IsRender = true;
            }
        }

        protected virtual bool AfterHandleEventShouldRender()
        {
            return true;
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
