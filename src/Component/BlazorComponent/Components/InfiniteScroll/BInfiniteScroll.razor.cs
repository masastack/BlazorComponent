using System.Globalization;
using BlazorComponent.JSInterop;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace BlazorComponent;

public partial class BInfiniteScroll
{
    [Parameter, EditorRequired] public EventCallback OnLoadMore { get; set; }

    [Parameter, EditorRequired] public bool HasMore { get; set; }

    [Parameter] public StringNumber Threshold { get; set; } = 250;

    [Parameter] public RenderFragment<(bool HasMore, bool Failed, EventCallback Retry)> ChildContent { get; set; }

    private bool _loading;
    private bool _failed;

    protected string ParentSelector { get; private set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            ParentSelector = await JsInvokeAsync<string>(JsInteropConstants.GetScrollParentSelector, Ref);

            await JsInvokeAsync(JsInteropConstants.TriggerLoadingEventIfReachThreshold, Ref, ParentSelector, Threshold,
                DotNetObjectReference.Create(new Invoker(OnScroll)));
            await Js.AddHtmlElementEventListener(ParentSelector, "scroll", OnScroll, false, new EventListenerExtras(0, 100));

            StateHasChanged();

            var en = new CultureInfo("en-us");
            

            DateTime.UtcNow.ToString(en.DateTimeFormat);

        }
    }

    private void OnScroll()
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} OnScroll...........");
    }

    private async Task Retry()
    {
        _loading = true;

        try
        {
            await OnLoadMore.InvokeAsync();
            _failed = false;
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, "Failed to load more");
            _failed = true;
        }
        finally
        {
            _loading = false;
        }
    }
}
