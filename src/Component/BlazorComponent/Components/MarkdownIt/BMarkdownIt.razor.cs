namespace BlazorComponent;

public partial class BMarkdownIt : ComponentBase
{
    [Inject]
    protected MarkdownItProxy MarkdownItProxy { get; set; }

    [Parameter]
    public string? Key { get; set; }

    [Parameter]
    public Action<MarkdownItOptions>? Options { get; set; }

    [Parameter]
    public string? Source { get; set; }

    [Parameter]
    public Dictionary<string, string>? TagClassMap { get; set; }

    private string _mdHtml = string.Empty;
    private string? _prevSource;
    private bool _markdownItProxyAvailable;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevSource != Source)
        {
            _prevSource = Source;

            if (_markdownItProxyAvailable)
            {
                await Render();
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _markdownItProxyAvailable = true;
            await Init();
            await Render();
        }
    }

    private async Task Init()
    {
        var options = new MarkdownItOptions();
        Options?.Invoke(options);

        var tagClassMap = TagClassMap ?? new Dictionary<string, string>();

        await MarkdownItProxy.Init(options, tagClassMap, Key);
    }

    private async Task Render()
    {
        _mdHtml = await MarkdownItProxy.Render(Source);
        StateHasChanged();
    }
}
