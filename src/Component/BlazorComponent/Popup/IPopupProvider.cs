namespace BlazorComponent;

public interface IPopupProvider
{
    event EventHandler StateChanged;
    
    ProviderItem Add(Type componentType, Dictionary<string, object> attributes, object service, string serviceName);
    void Remove(ProviderItem item);
    IEnumerable<ProviderItem> GetItems();
}