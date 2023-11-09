namespace BlazorComponent.Utils
{
    public class DelayTask
    {
        private CancellationTokenSource _cancellationTokenSource = new();
        private bool _reset;

        /// <summary>
        /// Initialize a delay task with 300ms delay.
        /// </summary>
        public DelayTask() : this(300)
        {
        }

        /// <summary>
        /// Initialize a delay task with a custom delay with millisecond.
        /// </summary>
        /// <param name="delay"></param>
        public DelayTask(int delay)
        {
            Delay = delay;
        }

        public int Delay { get; }

        /// <summary>
        /// Invoke this method to cancel the last delay task if there are a lot of task in a short time.
        /// </summary>
        public void Reset()
        {
            _reset = true;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Run a task with a delay in a single thread.
        /// </summary>
        /// <param name="task"></param>
        public async Task RunAsync(Func<Task> task)
        {
            EnsureReset();

            await Task.Delay(Delay, _cancellationTokenSource.Token);
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
}
