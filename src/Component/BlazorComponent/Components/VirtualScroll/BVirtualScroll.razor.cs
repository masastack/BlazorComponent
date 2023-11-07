namespace BlazorComponent;

public abstract partial class BVirtualScroll<TItem>
{
    [Parameter]
    public ICollection<TItem>? Items { get; set; }

    [Parameter]
    public RenderFragment<TItem>? ItemContent { get; set; }

    [Parameter]
    public RenderFragment? FooterContent { get; set; }

    [Parameter, MassApiParameter(50)]
    public float ItemSize { get; set; } = 50;

    [Parameter, MassApiParameter(3)]
    public int OverscanCount { get; set; } = 3;
}