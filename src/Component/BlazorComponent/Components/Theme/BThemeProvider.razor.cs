namespace BlazorComponent;

public partial class BThemeProvider : BDomComponentBase
{
    protected string? ThemeString { get; set; }

    [Parameter]
    public bool Default { get; set; }

    [Parameter]
    public Theme? Theme { get; set; }

    [Parameter]
    public bool? Dark { get; set; }

    [Parameter]
    public bool? RTL { get; set; }

    [Inject]
    public IThemeService ThemeService { get; set; } = null!;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        if (!Default)
        {
            BuildTheme();
        }

        ThemeService.Register(this);
    }

    protected override void OnParametersSet()
    {
        if (Dark != null)
        {
            ThemeService.Dark = Dark.Value;
        }
        if (RTL != null)
        {
            ThemeService.RTL = RTL.Value;
        }
    }

    public void UpdateTheme(bool force)
    {
        if (!Default || force)
        {
            BuildTheme();
            InvokeStateHasChanged();
        }
    }

    public void BuildTheme()
    {
        var theme = Theme ?? ThemeService.Theme;
        var themeOptions = ThemeService.Dark ? theme.DarkPalette : theme.LightPalette;
        ThemeString = ThemeCssBuilder.Build(themeOptions);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        ThemeService.UnRegister(this);
    }
}
