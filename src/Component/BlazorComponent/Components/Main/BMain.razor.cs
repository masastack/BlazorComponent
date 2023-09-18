namespace BlazorComponent;

public abstract partial class BMain : BDomComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}