namespace BlazorComponent.Mixins;

public interface IDependent
{
    void RegisterChild(IDependent dependent);

    IEnumerable<string> DependentElements { get; }
}