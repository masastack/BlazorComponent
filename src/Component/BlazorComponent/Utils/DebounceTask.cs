namespace BlazorComponent.Utils;

/// <summary>
/// TODO: delete this class
/// </summary>
public class DebounceTask
{
    private CancellationTokenSource _cancellationTokenSource = new();
    private bool _reset;

    /// <summary>
    /// Initialize a delay task with 300ms delay.
    /// </summary>
    public DebounceTask() : this(300)
    {
    }

    /// <summary>
    /// Initialize a delay task with a custom delay with millisecond.
    /// </summary>
    /// <param name="delay"></param>
    public DebounceTask(int delay)
    {
        Delay = delay;
    }

    public int Delay { get; }

    /// <summary>
    /// Cancel the latest task and re-calculate the delay.
    /// </summary>
    public void Reset()
    {
        _reset = true;

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    /// <summary>
    /// Run a task, call <see cref="Reset"/> before invoke this method.
    /// </summary>
    /// <param name="task"></param>
    public async Task RunAsync(Func<Task> task)
    {

        // Reset();
        EnsureReset();

        try
        {
            await Task.Delay(Delay, _cancellationTokenSource.Token);
        }
        catch(TaskCanceledException)
        {
            // ignored
        }

        await task.Invoke();
    }
    

    private void EnsureReset()
    {
        if (!_reset)
        {
            throw new InvalidOperationException("Before invoke RunAsync, you should invoke Reset() first.");
        }

        _reset = false;
    }
}
