using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IGroupable
    {
        StringNumber Value { get; set; }
        
        bool Disabled { get; set; }
        
        ElementReference Ref { get; set; }
    }
}