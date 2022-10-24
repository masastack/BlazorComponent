using BlazorComponent.Applicationable;

namespace BlazorComponent.Services;

public interface IApplication
{
    void Update(TargetProp prop, double value);
}
