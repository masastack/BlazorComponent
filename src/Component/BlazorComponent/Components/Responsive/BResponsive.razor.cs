using Microsoft.AspNetCore.Components;

namespace BlazorComponent;

public partial class BResponsive : BDomComponentBase, IResponsive
{
    [Parameter]
    public StringNumber? AspectRatio { get; set; }

    [Parameter]
    public string? ContentClass { get; set; }

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    public StringNumber? MaxHeight { get; set; }

    [Parameter]
    public StringNumber? MinHeight { get; set; }

    [Parameter]
    public StringNumber? Width { get; set; }

    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    [Parameter]
    public StringNumber? MinWidth { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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
}