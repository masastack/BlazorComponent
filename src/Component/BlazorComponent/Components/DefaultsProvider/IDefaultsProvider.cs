namespace BlazorComponent;

public interface IDefaultsProvider
{
    IDictionary<string , IDictionary<string, object?>?>? Defaults { get; }
}
