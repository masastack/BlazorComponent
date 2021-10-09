using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IItem
    {
        StringNumber Value { get; }

        RenderFragment ChildContent { get; }
    }
}