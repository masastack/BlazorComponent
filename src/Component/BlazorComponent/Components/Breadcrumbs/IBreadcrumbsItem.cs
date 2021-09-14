using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IBreadcrumbsItem: IHasProviderComponent
    {
        string Text { get; }
        
        string Href { get; }
        
        string Target { get; }
        
        bool Disabled { get; }
        
        RenderFragment ChildContent { get; }
    }
}