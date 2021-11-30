using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class BComponentBase : ComponentBase, IDisposable
    {
        private readonly Queue<Func<Task>> _afterRenderCallQuene = new Queue<Func<Task>>();

        [Parameter]
        public ForwardRef RefBack { get; set; } = new ForwardRef();

        [Inject]
        public virtual IJSRuntime Js { get; set; }

        protected bool Prerendering { get; set; } = true;

        protected void CallAfterRender(Func<Task> action)
        {
            _afterRenderCallQuene.Enqueue(action);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Prerendering = false;
            
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await OnFirstAfterRenderAsync();
            }

            if (_afterRenderCallQuene.Count > 0)
            {
                var actions = _afterRenderCallQuene.ToArray();
                _afterRenderCallQuene.Clear();

                foreach (var action in actions)
                {
                    if (IsDisposed)
                    {
                        return;
                    }

                    await action();
                }
            }
        }

        protected virtual Task OnFirstAfterRenderAsync()
        {
            return Task.CompletedTask;
        }

        protected void InvokeStateHasChanged()
        {
            InvokeAsync(() =>
            {
                if (!IsDisposed)
                {
                    StateHasChanged();
                }
            });
        }

        protected async Task InvokeStateHasChangedAsync()
        {
            await InvokeAsync(() =>
            {
                if (!IsDisposed)
                {
                    StateHasChanged();
                }
            });
        }

        protected async Task<T> JsInvokeAsync<T>(string code, params object[] args)
        {
            return await Js.InvokeAsync<T>(code, args);
        }

        protected async Task JsInvokeAsync(string code, params object[] args)
        {
            await Js.InvokeVoidAsync(code, args);
        }

        #region Dispose

        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BComponentBase()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        #endregion
    }
}
