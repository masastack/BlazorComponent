namespace BlazorComponent;

public partial class BHover : BActivatableBase
{
    [Parameter]
    public RenderFragment<HoverProps>? ChildContent { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        OpenOnHover = true;
        OpenOnClick = false;
    }
}
