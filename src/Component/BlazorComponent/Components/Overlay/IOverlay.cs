namespace BlazorComponent;

public interface IOverlay : IHasProviderComponent
{
    bool Value { get; }
    
    bool Scrim { get; }

    RenderFragment? ChildContent { get; }
        
    ElementReference ContentRef { set; }
}