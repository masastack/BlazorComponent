using System.Runtime.CompilerServices;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public abstract class BMenuable : BActivatable, IMenuable, IAsyncDisposable
{
    private const int BROWSER_RENDER_INTERVAL = 16;

    private readonly int _stackMinZIndex = 6;

    private bool _delayIsActive;

    private double _absoluteX;
    private double _absoluteY;
    private Web.Element _documentElement;
    private bool _hasWindow;
    protected bool _showContentCompleted;
    private Window _window;

    protected(Position activator, Position content) Dimensions = new(new Position(), new Position());

    protected bool ActivatorFixed { get; set; }

    protected double ComputedLeft
    {
        get
        {
            var a = Dimensions.activator;
            var c = Dimensions.content;
            var activatorLeft = Attach != null ? a.OffsetLeft : a.Left;
            var minWidth = Math.Max(a.Width, c.Width);

            double left = 0;
            left += Left ? activatorLeft - (minWidth - a.Width) : activatorLeft;

            if (OffsetX)
            {
                double maxWidth = 0;

                if (MaxWidth != null)
                {
                    (var isNumber, maxWidth) = MaxWidth.TryGetNumber();
                    maxWidth = isNumber ? Math.Min(a.Width, maxWidth) : a.Width;
                }

                left += Left ? -maxWidth : a.Width;
            }

            if (NudgeLeft != null)
            {
                var (_, nudgeLeft) = NudgeLeft.TryGetNumber();
                left -= nudgeLeft;
            }

            if (NudgeRight != null)
            {
                var (_, nudgeRight) = NudgeRight.TryGetNumber();
                left += nudgeRight;
            }

            return left;
        }
    }

    protected double ComputedTop
    {
        get
        {
            var a = Dimensions.activator;
            var c = Dimensions.content;

            double top = 0;

            if (Top) top += a.Height - c.Height;

            if (Attach != null)
                top += a.OffsetTop;
            else
                top += a.Top + PageYOffset;

            if (OffsetY) top += Top ? -a.Height : a.Height;

            if (NudgeTop != null)
            {
                var (isNumber, nudgeTop) = NudgeTop.TryGetNumber();
                if (isNumber)
                {
                    top -= nudgeTop;
                }
            }

            if (NudgeBottom != null)
            {
                var (isNumber, nudgeBottom) = NudgeBottom.TryGetNumber();
                if (isNumber)
                {
                    top += nudgeBottom;
                }
            }

            return top;
        }
    }

    protected int InternalZIndex { get; set; }

    protected override bool IsActive
    {
        get => _delayIsActive;
        set
        {
            if (Disabled) return;

            if (_delayIsActive == value) return;

            _ = value
                ? CallActivate(() => _delayIsActive = true)
                : CallDeactivate(() => _delayIsActive = false);
        }
    }

    protected double PageYOffset { get; set; }

    protected double PageWidth { get; set; }

    public ElementReference ContentRef { get; set; }

    public bool ShowContent { get; set; }

    [Inject]
    public DomEventJsInterop DomEventJsInterop { get; set; }

    [Parameter]
    public bool Absolute { get; set; }

    [Parameter]
    public bool AllowOverflow { get; set; }

    [Parameter]
    public string Attach { get; set; }

    [Parameter]
    public bool Bottom { get; set; }

    [Parameter]
    public bool Left { get; set; }

    [Parameter]
    public StringNumber MaxWidth { get; set; }

    [Parameter]
    public StringNumber MinWidth { get; set; }

    [Parameter]
    public StringNumber NudgeBottom { get; set; }

    [Parameter]
    public StringNumber NudgeLeft { get; set; }

    [Parameter]
    public StringNumber NudgeRight { get; set; }

    [Parameter]
    public StringNumber NudgeTop { get; set; }

    [Parameter]
    public StringNumber NudgeWidth { get; set; }

    [Parameter]
    public bool OffsetOverflow { get; set; }

    [Parameter]
    public bool OffsetX { get; set; }

    [Parameter]
    public bool OffsetY { get; set; }

    [Parameter]
    public bool OpenOnClick { get; set; } = true;

    [Parameter]
    public double? PositionX { get; set; }

    [Parameter]
    public double? PositionY { get; set; }

    [Parameter]
    public bool Right { get; set; }

    [Parameter]
    public bool Top { get; set; }

    [Parameter]
    public override bool Value
    {
        get => IsActive;
        set
        {
            _ = ShowLazyContent();

            IsActive = value;
        }
    }

    [Parameter]
    public StringNumber ZIndex { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _window = await JsInvokeAsync<Window>(JsInteropConstants.GetWindow);
            _documentElement = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, "document");

            _hasWindow = _window != null && _documentElement != null;

            if (_hasWindow)
            {
                DomEventJsInterop.AddEventListener<Window>("window", "resize", OnResize, false);
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    protected virtual async Task ShowLazyContent()
    {
        if (!ShowContent)
        {
            _showContentCompleted = false;

            ShowContent = true;

            await InvokeStateHasChangedAsync();
            await Task.Delay(BROWSER_RENDER_INTERVAL);

            await AfterShowContent();
            await MoveContentTo();
            await UpdateDimensions();

            _showContentCompleted = true;
        }
    }

    protected virtual Task AfterShowContent()
    {
        return Task.CompletedTask;
    }

    protected abstract Task MoveContentTo();

    protected virtual Task Activate(Action lazySetter)
    {
        lazySetter();

        return Task.CompletedTask;
    }

    public virtual string AttachedSelector => Attach;

    protected string CalcLeft(double menuWidth)
    {
        return ((StringNumber)(Attach != null ? ComputedLeft : CalcXOverflow(ComputedLeft, menuWidth))).ConvertToUnit();
    }

    protected string CalcTop()
    {
        return ((StringNumber)(Attach != null ? ComputedTop : CalcYOverflow(ComputedTop))).ConvertToUnit();
    }

    protected double CalcXOverflow(double left, double menuWidth)
    {
        var xOverflow = left + menuWidth - PageWidth + 12;

        if ((!Left || Right) && xOverflow > 0)
        {
            left = Math.Max(left - xOverflow, 0);
        }
        else
        {
            left = Math.Max(left, 12);
        }

        return left + GetOffsetLeft();
    }

    protected double CalcYOverflow(double top)
    {
        if (!_hasWindow) return 0;

        var documentHeight = GetInnerHeight();
        var toTop = PageYOffset + documentHeight;
        var activator = Dimensions.activator;
        var contentHeight = Dimensions.content.Height;
        var totalHeight = top + contentHeight;
        var isOverflowing = toTop < totalHeight;

        if (isOverflowing && OffsetOverflow && activator.Top > contentHeight)
        {
            top = PageYOffset + (activator.Top - contentHeight);
        }
        else if (isOverflowing && !AllowOverflow)
        {
            top = toTop - contentHeight - 12;
        }
        else if (top < PageYOffset && !AllowOverflow)
        {
            top = PageYOffset + 12;
        }

        return top < 12 ? 12 : top;
    }

    private async Task CallActivate(Action lazySetter)
    {
        if (!_hasWindow) return;

        await Activate(lazySetter);
    }

    private Task CallDeactivate(Action lazySetter)
    {
        return Deactivate(lazySetter);
    }

    private void CheckForPageYOffset()
    {
        if (_hasWindow)
        {
            PageYOffset = ActivatorFixed ? 0 : GetOffsetTop();
        }
    }

    private async Task CheckActivatorFixed()
    {
        ActivatorFixed = await JsInvokeAsync<bool>(JsInteropConstants.CheckElementFixed, ActivatorSelector);
    }

    protected virtual Task Deactivate(Action lazySetter)
    {
        lazySetter();
        return Task.CompletedTask;
    }

    protected override Dictionary<string, (EventCallback<MouseEventArgs> listener, EventListenerActions actions)> GenActivatorMouseListeners()
    {
        var listeners = base.GenActivatorMouseListeners();

        if (listeners.ContainsKey("click"))
        {
            var onClick = listeners["click"].listener;
            var actions = listeners["click"].actions;

            listeners["click"] = (CreateEventCallback<MouseEventArgs>(async e =>
            {
                await ShowLazyContent();

                if (OpenOnClick && onClick.HasDelegate)
                {
                    await onClick.InvokeAsync(e);
                }

                _absoluteX = e.ClientX;
                _absoluteY = e.ClientY;
            }), actions);
        }

        if (listeners.ContainsKey("mouseenter"))
        {
            var cb = listeners["mouseenter"].listener;
            var actions = listeners["mouseenter"].actions;

            listeners["mouseenter"] = (CreateEventCallback<MouseEventArgs>(async e =>
            {
                await ShowLazyContent();

                if (cb.HasDelegate)
                {
                    await cb.InvokeAsync(e);
                }
            }), actions);
        }

        if (listeners.ContainsKey("mouseleave"))
        {
            var cb = listeners["mouseleave"].listener;
            var actions = listeners["mouseleave"].actions;

            // ContentRef is null if use the feature ShowLazyContent
            if (ContentRef.Context != null)
            {
                if (actions == null)
                {
                    actions = new EventListenerActions(Document.GetElementByReference(ContentRef).Selector);
                }
                else
                {
                    actions.RelatedTarget = Document.GetElementByReference(ContentRef).Selector;
                }
            }

            listeners["mouseleave"] = (CreateEventCallback<MouseEventArgs>(async e =>
            {
                while (!_showContentCompleted)
                {
                    await Task.Delay(16);
                }

                while (!Value)
                {
                    await Task.Delay(16);
                }

                if (cb.HasDelegate)
                {
                    await cb.InvokeAsync(e);
                }
            }), actions);
        }

        return listeners;
    }

    private double GetInnerHeight()
    {
        if (!_hasWindow) return 0;

        return _window.InnerHeight > 0 ? _window.InnerHeight : _documentElement.ClientHeight;
    }

    private double GetOffsetLeft()
    {
        if (!_hasWindow) return 0;

        return _window.PageXOffset > 0 ? _window.PageXOffset : _documentElement.ScrollLeft;
    }

    private double GetOffsetTop()
    {
        if (!_hasWindow) return 0;

        return _window.PageYOffset > 0 ? _window.PageYOffset : _documentElement.ScrollTop;
    }

    private async void OnResize(Window window)
    {
        if (!IsActive) return;

        await Task.Run(() => UpdateDimensions());
    }

    private async Task<Position> Measure(HtmlElement element)
    {
        if (element == null || !_hasWindow) return null;

        var originRect = await element.GetBoundingClientRectAsync();

        var rect = new Position(originRect);

        if (Attach != null)
        {
            var marginLeft = "margin-left";
            var marginRight = "margin-right";

            var styles = await element.GetStylesAsync(marginLeft, marginRight);

            // TODO: check parse "2px"

            if (int.TryParse(styles[marginLeft], out var left))
            {
                rect!.Left = left;
            }

            if (int.TryParse(styles[marginRight], out var right))
            {
                rect!.Right = right;
            }
        }

        return rect;
    }

    protected async Task UpdateDimensions(Action lazySetter = null)
    {
        _window = await JsInvokeAsync<Window>(JsInteropConstants.GetWindow);
        _documentElement = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, "document");

        await CheckActivatorFixed();

        CheckForPageYOffset();

        PageWidth = _documentElement.ClientWidth;

        if (!HasActivator || Absolute)
        {
            Dimensions.activator = AbsolutePosition();
        }
        else
        {
            var activatorElement = GetActivator();
            var activator = await activatorElement.GetDomInfoAsync();

            Dimensions.activator = await Measure(activatorElement);
            Dimensions.activator.OffsetLeft = activator?.OffsetLeft ?? 0;

            if (Attach != null)
            {
                Dimensions.activator.OffsetTop = activator?.OffsetTop ?? 0;
            }
            else
            {
                Dimensions.activator.OffsetTop = 0;
            }
        }

        var contentElement = Document.GetElementByReference(ContentRef);
        Dimensions.content = await Measure(contentElement);

        lazySetter?.Invoke();

        InternalZIndex = await CalculateZIndex();

        await InvokeStateHasChangedAsync();
    }

    private Position AbsolutePosition() => new()
    {
        OffsetTop = PositionY ?? _absoluteY,
        OffsetLeft = PositionX ?? _absoluteX,
        ScrollHeight = 0,
        Top = PositionY ?? _absoluteY,
        Bottom = PositionY ?? _absoluteY,
        Left = PositionX ?? _absoluteX,
        Right = PositionX ?? _absoluteX,
        Height = 0,
        Width = 0
    };

    private async Task<int> ActiveZIndex()
    {
        return await GetMaxZIndex() + 2;
    }

    private async Task<int> CalculateZIndex()
    {
        if (ZIndex != null)
        {
            var (isNumber, number) = ZIndex.TryGetNumber();

            if (isNumber && number > 0)
            {
                return Convert.ToInt32(number);
            }
        }

        return await ActiveZIndex();
    }

    private async Task<int> GetMaxZIndex()
    {
        var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

        return maxZindex > _stackMinZIndex ? maxZindex : _stackMinZIndex;
    }

    private HtmlElement GetContent() => Document.GetElementByReference(ContentRef);

    private async Task DeleteContent()
    {
        try
        {
            if (ContentRef.Context != null)
            {
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, ContentRef, AttachedSelector);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hasWindow)
        {
            DomEventJsInterop.RemoveEventListener<Window>("window", "resize", OnResize);
        }

        if (ContentRef.Context != null)
        {
            object selectors = new[]
            {
                GetContent().Selector,
                ActivatorSelector
            };


            _ = JsInvokeAsync(JsInteropConstants.RemoveOutsideClickEventListener, selectors);
        }

        await DeleteContent();

        GC.SuppressFinalize(this);
    }

    protected class Position : BoundingClientRect
    {
        public double OffsetTop { get; set; }
        public double OffsetLeft { get; set; }
        public double ScrollHeight { get; set; }

        public Position()
        {
        }

        public Position(BoundingClientRect rect)
        {
            Bottom = rect?.Bottom ?? 0;
            Left = rect?.Left ?? 0;
            Height = rect?.Height ?? 0;
            Right = rect?.Right ?? 0;
            Top = rect?.Top ?? 0;
            Width = rect?.Width ?? 0;
            X = rect?.X ?? 0;
            Y = rect?.Y ?? 0;
        }
    }
}