namespace BlazorComponent;

public partial class BAlert : BDomComponentBase, IAlert, IThemeable
{
    public RenderFragment? IconContent { get; protected set; }

    public bool IsShowIcon { get; protected set; }

    [Parameter]
    public string? Transition { get; set; }

    [Parameter]
    public Borders Border { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    [MassApiParameter("$cancel")]
    public string CloseIcon { get; set; } = "$cancel";

    [Parameter]
    [MassApiParameter("Close")]
    public virtual string CloseLabel { get; set; } = "Close";

    [Parameter]
    public string? Color { get; set; }

    [Parameter]
    public virtual bool Dismissible { get; set; }

    [Parameter]
    [MassApiParameter("div")]
    public string Tag { get; set; } = "div";

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public RenderFragment? TitleContent { get; set; }

    [Parameter]
    public AlertTypes Type { get; set; }

    [Parameter]
    public bool Value { get; set; } = true;

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    public async Task HandleOnDismiss(MouseEventArgs args)
    {
        Value = false;
        await ValueChanged.InvokeAsync(false);
    }

    [MasaApiPublicMethod]
    public async Task ToggleAsync()
    {
        Value = !Value;
        await ValueChanged.InvokeAsync(Value);
    }
}
