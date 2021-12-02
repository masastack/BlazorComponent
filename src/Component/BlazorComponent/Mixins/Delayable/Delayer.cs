namespace BlazorComponent;

public class Delayer : IDelayable
{
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

    public int ComputedOpenDelay => OpenDelay == 0 ? 16 : OpenDelay;
    
    public int ComputedCloseDelay => CloseDelay == 0 ? 16 : CloseDelay;

    public async Task RunOpenDelay(Func<Task> cb = null)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        await Task.Delay(ComputedOpenDelay, _cancellationTokenSource.Token);

        if (cb != null)
        {
            await cb.Invoke();
        }
    }

    public async Task RunCloseDelay(Func<Task> cb = null)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        await Task.Delay(ComputedCloseDelay, _cancellationTokenSource.Token);

        if (cb != null)
        {
            await cb.Invoke();
        }
    }
}