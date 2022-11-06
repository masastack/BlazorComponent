namespace BlazorComponent;

public partial class BHighlight : BDomComponentBase
{
    [Inject]
    protected MarkdownItProxyModule MarkdownItProxyModule { get; set; }

    [Parameter]
    [EditorRequired]
    public string Code { get; set; }

    [Parameter]
    [EditorRequired]
    public string Language { get; set; }

    [Parameter]
    public bool Inline { get; set; }

    private const string Key = "Highlight";

    private string _codeHtml = string.Empty;
    private string? _prevCode;
    private MarkdownItProxy? _markdownItProxy;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevCode != Code)
        {
            _prevCode = Code;

            await TryHighlight();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await CreateMarkdownItProxy();
            await TryHighlight();
        }
    }

    private async Task CreateMarkdownItProxy()
    {
        var options = new MarkdownItOptions();

        _markdownItProxy = await MarkdownItProxyModule.Create(options, Key);
    }

    private async Task TryHighlight()
    {
        if (_markdownItProxy is null || Code is null) return;

        _codeHtml = await _markdownItProxy.Highlight(Code, Language);

        StateHasChanged();
    }
}
