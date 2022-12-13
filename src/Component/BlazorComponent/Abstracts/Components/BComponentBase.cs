using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract class BComponentBase : NextTickComponentBase, IHandleEvent
    {
        [Inject]
        [NotNull]
        public virtual IJSRuntime? Js { get; set; }

        [CascadingParameter]
        protected IErrorHandler? ErrorHandler { get; set; }

        [Parameter]
        public ForwardRef RefBack { get; set; } = new();

        private ParameterView ParameterView { get; set; }

        public override Task SetParametersAsync(ParameterView parameters)
        {
            ParameterView = parameters;

            return base.SetParametersAsync(parameters);
        }

        /// <summary>
        /// Check whether the parameter has been assigned value.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        protected bool IsDirtyParameter<TValue>(string parameterName)
        {
            return ParameterView.TryGetValue<TValue>(parameterName, out _);
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

        protected async Task<T> JsInvokeAsync<T>(string code, params object?[] args)
        {
            return await Js.InvokeAsync<T>(code, args);
        }

        protected async Task JsInvokeAsync(string code, params object?[] args)
        {
            await Js.InvokeVoidAsync(code, args);
        }

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
    }
}
