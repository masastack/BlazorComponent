using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class BComponentBase : ComponentBase, IDisposable
    {
        private readonly Queue<Func<Task>> _nextTickQuene = new();

        [Parameter]
        public ForwardRef RefBack { get; set; } = new ForwardRef();

        [Inject]
        public virtual IJSRuntime Js { get; set; }

        protected bool IsDisposed { get; private set; }

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

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            IsDisposed = true;
        }

        public void Dispose()
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
