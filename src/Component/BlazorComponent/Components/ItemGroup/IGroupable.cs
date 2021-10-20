using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IGroupable
    {
        string ActiveClass { get; set; }
        
        bool Disabled { get; set; }

        StringNumber Value { get; set; }
        
        ElementReference Ref { get; set; }
        
        string Class { get; set; }
        
        string Style { get; set; }
    }
}