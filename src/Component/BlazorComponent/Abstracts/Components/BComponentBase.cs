using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class BComponentBase : ComponentBase, IDisposable,IHandleEvent
    {
        private readonly Queue<Func<Task>> _nextTickQuene = new();

        [Parameter]
        public ForwardRef RefBack { get; set; } = new ForwardRef();

        [Inject]
        public virtual IJSRuntime Js { get; set; }

        protected bool IsDisposed { get; private set; }
       
        protected bool IsNotRender { get; set; }
        
        [CascadingParameter]
        public IErrorLogger? ErrorLogger { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_nextTickQuene.Count > 0)
            {
                var callbacks = _nextTickQuene.ToArray();
                _nextTickQuene.Clear();

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
            _nextTickQuene.Enqueue(callback);
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

                if (ErrorLogger != null)
                {
                    IsNotRender = true;
                    await ErrorLogger.HandlerExceptionAsync(ex);
                }
                else
                {
                    // 未开启全局捕获
                    throw;
                }
            }

            if (!IsNotRender)
            {
                StateHasChanged();
            }
            else
            {
                IsNotRender = false;
            }
        }

        Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
        {
            var task = callback.InvokeAsync(arg);
            var shouldAwaitTask = task.Status != TaskStatus.RanToCompletion &&
                task.Status != TaskStatus.Canceled;

            if (!IsNotRender)
            {
                // After each event, we synchronously re-render (unless !ShouldRender())
                // This just saves the developer the trouble of putting "StateHasChanged();"
                // at the end of every event callback.
                StateHasChanged();
            }
            else
            {
                IsNotRender = false;
            }

            return shouldAwaitTask ?
                CallStateHasChangedOnAsyncCompletion(task) :
                Task.CompletedTask;
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
