using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent;

public partial class Transition : ComponentBase
{
    [Parameter] public string? Name { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool LeaveAbsolute { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnBeforeEnter { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnEnter { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnAfterEnter { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnEnterCancelled { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnBeforeLeave { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnLeave { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnAfterLeave { get; set; }

    [Parameter] public Func<ElementReference, Task>? OnLeaveCancelled { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<Transition>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<Transition>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<Transition>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<Transition>.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}