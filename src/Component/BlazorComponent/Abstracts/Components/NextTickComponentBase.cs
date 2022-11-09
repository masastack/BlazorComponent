namespace BlazorComponent;

public class NextTickComponentBase : ComponentBase, IDisposable
{
    private readonly Queue<Func<Task>> _nextTickQueue = new();

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

    protected async Task NextTickWhile(Func<Task> callback, Func<bool> @while, int retryTimes = 20, CancellationToken cancellationToken = default)
    {
        if (retryTimes > 0 && !cancellationToken.IsCancellationRequested)
        {
            if (@while.Invoke())
            {
                retryTimes--;

                await Task.Delay(100, cancellationToken);

                await NextTickWhile(callback, @while, retryTimes, cancellationToken);
            }
            else
            {
                await callback.Invoke();
            }
        }
    }


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

    ~NextTickComponentBase()
    {
        // Finalizer calls Dispose(false)
        Dispose(false);
    }
}
