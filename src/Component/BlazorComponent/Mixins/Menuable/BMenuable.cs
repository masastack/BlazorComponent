using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public class BMenuable : BBootable, IActivatable
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool AllowOverflow { get; set; }

        [Parameter]
        public string ContentClass { get; set; }

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
        public bool OpenOnClick { get; set; } = true;

        [Parameter]
        public double? PositionX { get; set; }

        [Parameter]
        public double? PositionY { get; set; }

        [Parameter]
        public StringNumber ZIndex { get; set; }

        [Parameter]
        public string Attach { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool OffsetX { get; set; }

        [Parameter]
        public bool OffsetY { get; set; }

        [Parameter]
        public bool ExternalActivator { get; set; }

        [Inject]
        public Window Window { get; set; }

        [Inject]
        public Document Document { get; set; }

        protected double ComputedLeft
        {
            get
            {
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;
                var activatorLeft = Attach != null ? activator.OffsetLeft : activator.Left;
                var minWidth = Math.Max(activator.Width, content.Width);

                double left = 0;
                left += Left ? activatorLeft - (minWidth - activator.Width) : activatorLeft;

                if (OffsetX)
                {
                    double maxWidth = 0;

                    if (MaxWidth != null)
                    {
                        (var isNumber, maxWidth) = MaxWidth.TryGetNumber();
                        maxWidth = isNumber ? Math.Min(activator.Width, maxWidth) : activator.Width;
                    }

                    left += Left ? -maxWidth : activator.Width;
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
                var activator = Dimensions.Activator;
                var content = Dimensions.Content;

                double top = 0;

                if (Top) top += activator.Height - content.Height;

                if (Attach != null)
                {
                    top += activator.OffsetTop;
                }
                else
                {
                    top += activator.Top + PageYOffset;
                }

                if (OffsetY) top += Top ? -activator.Height : activator.Height;

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

        protected MenuablePosition AbsolutePosition => new()
        {
            OffsetTop = PositionY ?? AbsoluteY,
            OffsetLeft = PositionX ?? AbsoluteX,
            ScrollHeight = 0,
            Top = PositionY ?? AbsoluteY,
            Bottom = PositionY ?? AbsoluteY,
            Left = PositionX ?? AbsoluteX,
            Right = PositionX ?? AbsoluteX,
            Height = 0,
            Width = 0
        };

        protected double AbsoluteYOffset
        {
            get
            {
                return PageYOffset - RelativeYOffset;
            }
        }

        protected bool HasActivator => ActivatorContent != null || ExternalActivator;

        protected virtual string AttachSelector => Attach;

        protected int ComputedZIndex
        {
            get
            {
                return ZIndex != null ? ZIndex.ToInt32() : Math.Max(ActivateZIndex, StackMinZIndex);
            }
        }

        protected MenuableDimensions Dimensions { get; } = new MenuableDimensions();

        protected double AbsoluteX { get; set; }

        protected double AbsoluteY { get; set; }

        protected double PageYOffset { get; set; }

        protected double PageWidth { get; set; }

        protected double RelativeYOffset { get; set; }

        protected bool ActivatorFixed { get; set; }

        protected ElementReference ContentElement { get; set; }

        protected int ActivateZIndex { get; set; }

        protected int StackMinZIndex { get; set; } = 6;

        protected bool Attached { get; set; }

        protected StringNumber CalcLeft(double menuWidth)
        {
            var left = Attach != null ? ComputedLeft : CalcXOverflow(ComputedLeft, menuWidth);
            return left > 0 ? left : null;
        }

        protected StringNumber CalcTop()
        {
            var top = Attach != null ? ComputedTop : CalcYOverflow(ComputedTop);
            return top > 0 ? top : null;
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

        private double GetOffsetLeft()
        {
            return Window.PageXOffset > 0 ? Window.PageXOffset : Document.DocumentElement.ScrollLeft;
        }

        protected double CalcYOverflow(double top)
        {
            var documentHeight = GetInnerHeight();
            var toTop = PageYOffset + documentHeight;
            var activator = Dimensions.Activator;
            var contentHeight = Dimensions.Content.Height;
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
            else if (top < AbsoluteYOffset && !AllowOverflow)
            {
                top = AbsoluteYOffset + 12;
            }

            return top < 12 ? 12 : top;
        }

        private double GetInnerHeight()
        {
            return Window.InnerHeight > 0 ? Window.InnerHeight : Document.DocumentElement.ClientHeight;
        }

        private void CheckForPageYOffset()
        {
            PageYOffset = ActivatorFixed ? 0 : GetOffsetTop();
        }

        private double GetOffsetTop()
        {
            return Window.PageYOffset > 0 ? Window.PageYOffset : Document.DocumentElement.ScrollTop;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Window.OnResize += HandleOnResizeAsync;
        }

        private async Task HandleOnResizeAsync()
        {
            if (!IsActive)
            {
                return;
            }

            await InvokeAsync(async () =>
            {
                await UpdateDimensionsAsync();
                StateHasChanged();
            });
        }

        protected virtual async Task UpdateDimensionsAsync()
        {
            //Invoke multiple method
            //1、Attach
            //2、Window,Document
            //3、Dimensions
            //4、z-index
            var windowProps = new string[] { "innerHeight", "innerWidth", "pageXOffset", "pageYOffset" };
            var documentProps = new string[] { "clientHeight", "clientWidth", "scrollLeft", "scrollTop" };

            var hasActivator = HasActivator && !Absolute;
            var multipleResult = await JsInvokeAsync<MultipleResult>(JsInteropConstants.InvokeMultipleMethod, windowProps, documentProps, hasActivator, ActivatorSelector, Attach, ContentElement, Attached, AttachSelector, Ref);
            var windowAndDocument = multipleResult.WindowAndDocument;

            //We want to reduce js interop
            //And we attach content-element in last step
            if (!Attached)
            {
                ActivateZIndex = multipleResult.ZIndex;
                Attached = true;
            }

            //Window props
            Window.InnerHeight = windowAndDocument.InnerHeight;
            Window.InnerWidth = windowAndDocument.InnerWidth;
            Window.PageXOffset = windowAndDocument.PageXOffset;
            Window.PageYOffset = windowAndDocument.PageYOffset;

            //Document props
            Document.DocumentElement.ClientHeight = windowAndDocument.ClientHeight;
            Document.DocumentElement.ClientWidth = windowAndDocument.ClientWidth;
            Document.DocumentElement.ScrollLeft = windowAndDocument.ScrollLeft;
            Document.DocumentElement.ScrollTop = windowAndDocument.ScrollTop;

            //TODO:CheckActivatorFixed
            CheckForPageYOffset();
            PageWidth = Document.DocumentElement.ClientWidth;

            var dimensions = multipleResult.Dimensions;
            if (hasActivator)
            {
                Dimensions.Activator = dimensions.Activator;
            }
            else
            {
                Dimensions.Activator = AbsolutePosition;

                //Since no activator,we should re-computed it's top and left
                Dimensions.Activator.Top -= dimensions.RelativeYOffset;
                Dimensions.Activator.Left -= Window.PageXOffset + dimensions.OffsetParentLeft;
            }

            Dimensions.Content = dimensions.Content;
            RelativeYOffset = dimensions.RelativeYOffset;
        }

        protected override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            AbsoluteX = args.ClientX;
            AbsoluteY = args.ClientY;

            if (OpenOnClick)
            {
                await base.HandleOnClickAsync(args);
            }
        }

        protected override async Task OnActiveUpdated(bool value)
        {
            if (value)
            {
                await CallActivateAsync();
            }
            else
            {
                await CallDeactivateAsync();
            }

            await base.OnActiveUpdated(value);
        }

        private async Task CallActivateAsync()
        {
            await ActivateAsync();
        }

        protected virtual async Task ActivateAsync()
        {
            await UpdateDimensionsAsync();

            //Wait for left and top update
            //Otherwise,we may get a flash
            StateHasChanged();
            await Task.Delay(16);
        }

        private Task CallDeactivateAsync()
        {
            //TODO:isContentActive
            return DeactivateAsync();
        }

        protected virtual Task DeactivateAsync()
        {
            return Task.CompletedTask;
        }

        protected override void Dispose(bool disposing)
        {
            Window.OnResize -= HandleOnResizeAsync;
            base.Dispose(disposing);
        }
    }
}
