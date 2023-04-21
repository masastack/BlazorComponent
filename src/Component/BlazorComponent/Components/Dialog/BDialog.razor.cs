using BlazorComponent.JSInterop;
using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent;

public partial class BDialog : BBootable, IDependent, IAsyncDisposable
{
    [Inject]
    private OutsideClickJSModule? OutsideClickJsModule { get; set; }

    [CascadingParameter]
    public IDependent? CascadingDependent { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    [Parameter]
    public string? Attach { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public bool Fullscreen { get; set; }

    [Parameter]
    public bool HideOverlay { get; set; }

    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

    [Parameter]
    public bool Persistent { get; set; }

    [Parameter]
    public StringNumber? Width { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public bool Light { get; set; }

    private readonly List<IDependent> _dependents = new();

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

    protected bool ShowOverlay => !Fullscreen && !HideOverlay;

    protected ElementReference? OverlayRef => ((BOverlay)Overlay)?.Ref;

    protected int StackMinZIndex { get; set; } = 200;

    protected virtual string? AttachSelector => default;

    public ElementReference ContentRef { get; set; }

    public ElementReference DialogRef { get; set; }

    protected object Overlay { get; set; }

    protected int ZIndex { get; set; }

    protected bool Animated { get; set; }

    protected override async Task WhenIsActiveUpdating(bool value)
    {
        if (ContentRef.Context is not null)
        {
            await AttachAsync(value);
        }
        else
        {
            NextTick(() => AttachAsync(value));
        }

        if (value)
        {
            ZIndex = await GetActiveZIndex(true);

            await HideScroll();

            NextTick(async () =>
            {
                // TODO: previousActiveElement

                var contains = await JsInvokeAsync<bool>(JsInteropConstants.ContainsActiveElement, ContentRef);
                if (!contains)
                {
                    await JsInvokeAsync(JsInteropConstants.Focus, ContentRef);
                }
            });
        }
        else
        {
            await ShowScroll();
        }

        await base.WhenIsActiveUpdating(value);
    }

    private async Task AttachAsync(bool value)
    {
        if (OutsideClickJsModule is { Initialized: false })
        {
            await OutsideClickJsModule.InitializeAsync(this, DependentSelectors.ToArray());

            await JsInvokeAsync(JsInteropConstants.AddElementTo, OverlayRef, AttachSelector);
            await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, AttachSelector);
        }
    }

    protected virtual bool IsFullscreen => Fullscreen;

    private async Task HideScroll()
    {
        await JsInvokeAsync(JsInteropConstants.HideScroll, IsFullscreen, OverlayRef.GetSelector(), ContentRef, DialogRef);
    }

    private async Task ShowScroll()
    {
        await JsInvokeAsync(JsInteropConstants.ShowScroll, OverlayRef.GetSelector());
    }

    private async Task<int> GetActiveZIndex(bool isActive)
    {
        return !isActive ? await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef) : await GetMaxZIndex() + 2;
    }

    private async Task<int> GetMaxZIndex()
    {
        var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

        return maxZindex > StackMinZIndex ? maxZindex : StackMinZIndex;
    }

    public async Task Keydown(KeyboardEventArgs args)
    {
        if (args.Key == "Escape")
        {
            Close();
        }
    }

    private void Close()
    {
        if (Persistent)
        {
            Animated = true;
            StateHasChanged();
            NextTick(async () =>
            {
                //This animated need 150ms
                await Task.Delay(150);
                Animated = false;
                StateHasChanged();
            });
        }
        else
        {
            RunDirectly(false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DeleteContent();
    }

    protected virtual async Task DeleteContent()
    {
        try
        {
            if (IsActive)
            {
                await ShowScroll();
            }

            if (ContentRef.Context != null)
            {
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, ContentRef, AttachSelector);
            }

            if (OverlayRef?.Context != null)
            {
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, OverlayRef, AttachSelector);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public override async Task HandleOnOutsideClickAsync()
    {
        var maxZIndex = await GetMaxZIndex();

        // TODO: should ignore the click if e.target was dragged onto the overlay
        if (IsActive && ZIndex >= maxZIndex)
        {
            await OnOutsideClick.InvokeAsync();

            Close();
        }
    }

    public void RegisterChild(IDependent dependent)
    {
        _dependents.Add(dependent);

        OutsideClickJsModule?.UpdateDependentElements(DependentSelectors.ToArray());
    }

    public virtual IEnumerable<string> DependentSelectors
    {
        get
        {
            var elements = _dependents.SelectMany(dependent => dependent.DependentSelectors).ToList();

            elements.Add(ContentRef.GetSelector());

            return elements;
        }
    }
}
