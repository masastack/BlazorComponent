namespace BlazorComponent
{
    public abstract class BComponentBase : NextTickComponentBase, IHandleEvent
    {
        [Inject]
        [NotNull]
        public virtual IJSRuntime? Js { get; set; }

        [CascadingParameter]
        protected IDefaultsProvider? DefaultsProvider { get; set; }

        [CascadingParameter]
        protected IErrorHandler? ErrorHandler { get; set; }

        [Parameter]
        public ForwardRef RefBack
        {
            get => _refBack ??= new ForwardRef();
            set => _refBack = value;
        }

        private ForwardRef? _refBack;
        private bool _shouldRender = true;
        private string[] _dirtyParameters = Array.Empty<string>();

        protected virtual string ComponentName
        {
            get
            {
                var type = this.GetType();
                return type.IsGenericType ? type.Name.Split('`')[0] : type.Name;
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            _dirtyParameters = parameters.ToDictionary().Keys.ToArray();

            if (parameters.TryGetValue<IDefaultsProvider>(nameof(DefaultsProvider), out var defaultsProvider)
                && defaultsProvider.Defaults is not null
                && defaultsProvider.Defaults.TryGetValue(ComponentName, out var dictionary)
                && dictionary is not null)
            {
                var defaults = ParameterView.FromDictionary(dictionary);
                defaults.SetParameterProperties(this);
            }

            await base.SetParametersAsync(parameters);
        }

        protected override bool ShouldRender() => _shouldRender;

        /// <summary>
        /// Check whether the parameter has been assigned value.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        protected bool IsDirtyParameter(string parameterName)
        {
            return _dirtyParameters.Contains(parameterName);
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

        protected void PreventRenderingUtil(params Action[] actions)
        {
            _shouldRender = false;
            actions.ForEach(action => action());
            _shouldRender = true;
        }

        protected async Task PreventRenderingUtil(params Func<Task>[] funcs)
        {
            _shouldRender = false;
            await funcs.ForEachAsync(func => func());
            _shouldRender = true;
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
