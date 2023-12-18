using BlazorComponent.Abstracts;

namespace BlazorComponent;

public class CssProviderComponentBase : ComponentBase
{
    protected CssProviderComponentBase()
    {
        CssProvider = new(() => Class, () => Style);
    }

    public ComponentCssProvider CssProvider { get; }

    /// <summary>
    /// Specifies one or more class names for an DOM element.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Specifies an inline style for an DOM element.
    /// </summary>
    [Parameter]
    public string? Style { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        SetComponentClass();
        SetComponentCss();
    }

    [Obsolete("Use SetComponentCss instead")]
    protected virtual void SetComponentClass()
    {
    }

    protected virtual void SetComponentCss()
    {
    }
}
