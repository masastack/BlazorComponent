namespace BlazorComponent;

public class ProviderItem
{
    public Type ComponentType { get; set; }

    public PopupProvider Provider { get; set; }
    
    public TaskCompletionSource<object> TaskCompletionSource { get; set; }

    public object Service { get; set; }

    public string ServiceName { get; set; }

    public Dictionary<string, object> Parameters { get; set; }

    public ProviderItem()
    {
        TaskCompletionSource = new();
        Parameters = new();
    }

    public void Discard(object result)
    {
        TaskCompletionSource.TrySetResult(result);
        Provider.Remove(this);
    }
}