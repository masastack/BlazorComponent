using BlazorComponent.JSInterop;
using BlazorComponent.Web;
using Microsoft.Extensions.Logging;

namespace BlazorComponent;

public partial class BInfiniteScroll : BDomComponentBase
{
    [Parameter, EditorRequired]
    public EventCallback OnLoadMore { get; set; }

    [Parameter, EditorRequired]
    public bool HasMore { get; set; }

    /// <summary>
    /// The parent element that has overflow style.
    /// </summary>
    [Parameter, EditorRequired]
    public ElementReference? Parent { get; set; }

    [Parameter]
    public StringNumber Threshold { get; set; } = 250;

    [Parameter]
    public RenderFragment<(bool HasMore, bool Failed, EventCallback Retry)> ChildContent { get; set; }

    [Parameter]
    public string NoMoreText { get; set; }

    [Parameter]
    public string FailedToLoadText { get; set; }

    [Parameter]
    public string LoadingText { get; set; }

    [Parameter]
    public string ReloadText { get; set; }

    private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);

    private bool _loading;
    private bool _failed;
    private bool _isAttached;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (!_isAttached && Parent?.Context is not null)
        {
            _isAttached = true;

            await Js.AddHtmlElementEventListener(Parent.GetSelector(), "scroll", OnScroll, false, new EventListenerExtras(0, 100));

            // Run manually once to check whether the threshold is exceeded.
            // Use NextTick to wait for the list rendering to complete,
            // otherwise we will get the wrong top for the first time.
            NextTick(OnScroll);
        }
    }

    private async Task OnScroll()
    {
        if (!OnLoadMore.HasDelegate) return;

        await SemaphoreSlim.WaitAsync();

        if (_failed)
        {
            SemaphoreSlim.Release();
            return;
        }

        // OPTIMIZE: Combine scroll event and the following js interop.
        var exceeded = await JsInvokeAsync<bool>(JsInteropConstants.CheckIfThresholdIsExceededWhenScrolling, Ref, Parent, Threshold.ToDouble());
        if (!exceeded)
        {
            SemaphoreSlim.Release();
            return;
        }

        await DoLoadMore();

        SemaphoreSlim.Release();
    }

    private async Task DoLoadMore()
    {
        try
        {
            _failed = false;
            await OnLoadMore.InvokeAsync();
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, "Failed to load more");
            _failed = true;
            StateHasChanged();
        }
    }

    private async Task Retry()
    {
        _loading = true;

        await DoLoadMore();

        _loading = false;
    }
}
