using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace BlazorComponent;

public class GridstackProxyModule : JSModule
{
    private readonly DotNetObjectReference<GridstackProxyModule> _dotNetObjectReference;

    public GridstackProxyModule(IJSRuntime js) : base(js, "./_content/BlazorComponent/js/gridstack-proxy.js")
    {
        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    public event EventHandler<GridstackResizeEventArgs>? Resize;

    public async ValueTask<IJSObjectReference> Init(object options, ElementReference el)
        => await InvokeAsync<IJSObjectReference>("init", options, el, _dotNetObjectReference);

    public async ValueTask SetStatic(IJSObjectReference instance, bool staticValue)
        => await InvokeVoidAsync("setStatic", instance, staticValue);

    public async ValueTask<IJSObjectReference> Reload(IJSObjectReference instance)
        => await InvokeAsync<IJSObjectReference>("reload", instance);

    [JSInvokable]
    public void OnResize(string? blazorId, string id, int width, int height)
    {
        if (blazorId is not null)
        {
            var el = new ElementReference(blazorId);
            var eventArgs = new GridstackResizeEventArgs(el, id, width, height);
            Resize?.Invoke(this, eventArgs);
        }
        else
        {
            Resize?.Invoke(this, new GridstackResizeEventArgs(id, width, height));
        }
    }
    
    [JSInvokable]
    public void OnResizeStop(string? blazorId, string id, int width, int height)
    {
        if (blazorId is not null)
        {
            var el = new ElementReference(blazorId);
            var eventArgs = new GridstackResizeEventArgs(el, id, width, height);
            Resize?.Invoke(this, eventArgs);
        }
        else
        {
            Resize?.Invoke(this, new GridstackResizeEventArgs(id, width, height));
        }
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        _dotNetObjectReference.Dispose();
    }

    public class GridstackResizeEventArgs : EventArgs
    {
        private readonly ElementReference _elementReference;

        public GridstackResizeEventArgs(ElementReference elementReference, string? id, int width, int height) : this(id, width, height)
        {
            _elementReference = elementReference;
        }

        public GridstackResizeEventArgs(string? id, int width, int height)
        {
            Id = id;
            Width = width;
            Height = height;
        }

        public string? Id { get; }

        public int Width { get; }

        public int Height { get; }

        public ElementReference? ElementReference => _elementReference;
    }
}
