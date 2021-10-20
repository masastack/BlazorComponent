using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IItem : IGroupable
    {
        RenderFragment ChildContent { get; set; }
    }
}