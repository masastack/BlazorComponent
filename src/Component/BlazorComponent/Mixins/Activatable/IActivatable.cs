using Microsoft.AspNetCore.Components;

namespace BlazorComponent;

public interface IActivatable
{
    public RenderFragment ComputedActivatorContent { get; }
    
    public bool Value { get; }
}