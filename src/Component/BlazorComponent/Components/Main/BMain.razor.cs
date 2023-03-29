namespace BlazorComponent;

public abstract partial class BMain : BDomComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Determines if the DOM element is ready to be displayed.
    /// </summary>
    protected virtual bool IsBooted => true;
}