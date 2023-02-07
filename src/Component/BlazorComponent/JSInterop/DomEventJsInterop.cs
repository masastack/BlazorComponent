using Microsoft.JSInterop;

namespace BlazorComponent
{
    public class DomEventJsInterop : IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly List<DotNetObjectReference<Invoker>> _references = new();

        public DomEventJsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task IntersectionObserver(string selector, params Func<Task>[] funcList)
        {
            List<DotNetObjectReference<Invoker>> invokers = new();

            foreach (var func in funcList)
            {
                invokers.Add(DotNetObjectReference.Create(new Invoker(func)));
            }

            _references.AddRange(invokers);

            await _jsRuntime.InvokeVoidAsync(
                JsInteropConstants.IntersectionObserver,
                selector,
                invokers);
        }

        public async Task ResizeObserver(string selector, Func<Task> func)
        {
            var invoker = DotNetObjectReference.Create(new Invoker(func));

            _references.Add(invoker);

            await _jsRuntime.InvokeVoidAsync(
                JsInteropConstants.ResizeObserver,
                selector,
                invoker);
        }

        public void Dispose()
        {
            foreach (var reference in _references)
            {
                reference.Dispose();
            }
        }
    }

    public class Invoker
    {
        private readonly Action? _action;
        private readonly Func<Task>? _func;

        public Invoker(Action action)
        {
            _action = action;
        }

        public Invoker(Func<Task> func)
        {
            _func = func;
        }

        [JSInvokable]
        public async Task Invoke()
        {
            if (_action != null)
            {
                _action.Invoke();
            }
            else if (_func != null)
            {
                await _func.Invoke();
            }
        }
    }

    public class Invoker<T>
    {
        private readonly Action<T>? _action;
        private readonly Func<T, Task>? _func;

        public Invoker(Action<T> action)
        {
            _action = action;
        }

        public Invoker(Func<T, Task> func)
        {
            _func = func;
        }

        [JSInvokable]
        public async Task Invoke(T param)
        {
            if (_action != null)
            {
                _action.Invoke(param);
            }
            else if (_func != null)
            {
                await _func(param);
            }
        }
    }
}
