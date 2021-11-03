using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorComponent;

public class Delayer : IDelayable
{
    private const int MinDelay = 100;

    private CancellationTokenSource _cancellationTokenSource;

    public Delayer()
    {
    }

    public Delayer(IDelayable delayable)
    {
        OpenDelay = delayable.OpenDelay;
        CloseDelay = delayable.CloseDelay;
    }

    public int OpenDelay { get; set; }

    public int CloseDelay { get; set; }

    public async Task RunOpenDelay(Func<Task> cb = null)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        var delay = MinDelay + OpenDelay;
        await Task.Delay(delay, _cancellationTokenSource.Token);

        if (cb != null)
        {
            await cb.Invoke();
        }
    }

    public async Task RunCloseDelay(Func<Task> cb = null)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        var delay = MinDelay + CloseDelay;
        await Task.Delay(delay, _cancellationTokenSource.Token);

        if (cb != null)
        {
            await cb.Invoke();
        }
    }
}