namespace BlazorComponent;

public partial class BHover : BActivatableBase
{
    [Parameter]
    public RenderFragment<HoverProps>? ChildContent { get; set; }

    public override Dictionary<string, object> ActivatorAttributes => new()
    {
        { ActivatorId, true }
    };

    protected override void OnInitialized()
    {
        base.OnInitialized();

        OpenOnHover = true;
        OpenOnClick = false;
    }
}
