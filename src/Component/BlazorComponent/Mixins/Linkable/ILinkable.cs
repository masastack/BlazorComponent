using Microsoft.AspNetCore.Components;

namespace BlazorComponent;

public interface ILinkable
{
    bool Exact { get; }
    
    bool Linkage { get; }
    
    NavigationManager NavigationManager { get; }
}