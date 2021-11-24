using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSlideGroup : BItemGroup, ISlideGroup
    {
        public BSlideGroup() : base(GroupType.SlideGroup)
        {
        }

        public BSlideGroup(GroupType groupType) : base(groupType)
        {
        }

        protected bool IsMobile { get; set; }

        protected ElementReference WrapperRef { get; set; }

        protected ElementReference ContentRef { get; set; }

        protected double WrapperWidth { get; set; }

        protected double ContentWidth { get; set; }

        private double _scrollOffset;

        protected double ScrollOffset
        {
            get => _scrollOffset;
            set
            {
                _scrollOffset = value;

                if (ContentRef.Context != null)
                {
                    _ = JsInvokeAsync(JsInteropConstants.SetStyle, ContentRef, "transform", $"translateX(-{value}px)");
                }
            }
        }

        protected double StartX { get; set; }

        [CascadingParameter(Name = "rtl")]
        public bool Rtl { get; set; }

        [Parameter]
        public bool CenterActive { get; set; }

        [Parameter]
        public StringBoolean ShowArrows { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public RenderFragment NextContent { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public RenderFragment PrevContent { get; set; }

        protected bool _render;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IsMobile = await JsInvokeAsync<bool>(JsInteropConstants.IsMobile);
            }

            if (!_render)
            {
                await SetWidths();
            }
        }

        public async Task SetWidths()
        {
            (WrapperWidth, ContentWidth) = await GetWidths();

            IsOverflowing = WrapperWidth + 1 < ContentWidth;

            await ScrollToView();

            _render = true;

            StateHasChanged();
        }

        private async Task<(double wrapper, double content)> GetWidths()
        {
            var wrapperElement = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, WrapperRef);
            var contentElement = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, ContentRef);
            return (wrapperElement?.ClientWidth ?? 0, contentElement?.ClientWidth ?? 0);
        }

        protected override void SetComponentClass()
        {
            AbstractProvider.Apply(typeof(CascadingValue<ISlideGroup>), typeof(CascadingValue<BSlideGroup>),
                props => props[nameof(CascadingValue<BSlideGroup>.Value)] = this);
        }

        public bool IsOverflowing { get; protected set; }

        public bool HasAffixes
        {
            get
            {
                var _ = !IsMobile && (IsOverflowing || Math.Abs(ScrollOffset) > 0);

                if (ShowArrows == null) return _;

                return ShowArrows.Match(
                    str =>
                    {
                        return str switch
                        {
                            "always" => true, // Always show arrows on desktop & mobile
                            "desktop" => !IsMobile, // Always show arrows on desktop
                            "mobile" => IsMobile || (IsOverflowing || Math.Abs(ScrollOffset) > 0), // Show arrows on mobile when overflowing.
                            _ => _
                        };
                    },
                    @bool =>
                    {
                        return @bool switch
                        {
                            true => IsOverflowing || Math.Abs(ScrollOffset) > 0, // Always show on mobile
                            _ => _
                        };
                    });
            }
        }

        public bool HasNext => HasAffixes && (ContentWidth > Math.Abs(ScrollOffset) + WrapperWidth);

        public bool HasPrev => HasAffixes && ScrollOffset != 0;

        async Task ISlideGroup.OnAffixClick(string direction)
        {
            await ScrollTo(direction);

            StateHasChanged();
        }

        protected async Task ScrollToView()
        {
            if (Value == null && Items.Any())
            {
                var lastItemRef = Items[^1].Ref;
                if (lastItemRef.Context == null) return;

                var lastItemPosition = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, lastItemRef);
                var wrapperPosition = await JsInvokeAsync<BoundingClientRect>(JsInteropConstants.GetBoundingClientRect, WrapperRef);

                if ((Rtl && wrapperPosition.Right < lastItemPosition.Right) || (!Rtl && wrapperPosition.Left > lastItemPosition.Left))
                {
                    await ScrollTo("prev");
                }
            }

            if (Value == null) return;

            var selectedItem = Items.FirstOrDefault(item => item.Value == Value);
            if (selectedItem?.Ref.Context == null) return;

            if (Items.FindIndex(u => u.Value == Value) == 0 || (!CenterActive && !IsOverflowing))
            {
                ScrollOffset = 0;
            }
            else if (CenterActive)
            {
                ScrollOffset = await CalculateCenteredOffset(selectedItem.Ref, WrapperWidth, ContentWidth, Rtl);
            }
            else if (IsOverflowing)
            {
                ScrollOffset = await CalculateUpdatedOffset(selectedItem.Ref, WrapperWidth, ContentWidth, Rtl, ScrollOffset);
            }
        }

        protected async Task<double> CalculateUpdatedOffset(ElementReference selected, double wrapperWidth, double contentWidth, bool rtl,
            double currentScrollOffset)
        {
            var selectedDomInfo = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, selected);
            var clientWidth = selectedDomInfo.ClientWidth;
            var offsetLeft = rtl ? (contentWidth - selectedDomInfo.OffsetLeft - clientWidth) : selectedDomInfo.OffsetLeft;

            if (rtl)
            {
                currentScrollOffset = -currentScrollOffset;
            }

            var totalWidth = wrapperWidth + currentScrollOffset;
            var itemOffset = clientWidth + offsetLeft;
            var additionalOffset = clientWidth * 0.4;

            if (offsetLeft <= currentScrollOffset)
            {
                currentScrollOffset = Math.Max(offsetLeft - additionalOffset, 0);
            }
            else if (totalWidth <= itemOffset)
            {
                currentScrollOffset = Math.Min(currentScrollOffset - (totalWidth - itemOffset - additionalOffset), contentWidth - wrapperWidth);
            }

            return rtl ? -currentScrollOffset : currentScrollOffset;
        }

        protected async Task<double> CalculateCenteredOffset(ElementReference selected, double wrapperWidth, double contentWidth, bool rtl)
        {
            var selectedDomInfo = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, selected);
            var offsetLeft = selectedDomInfo.OffsetLeft;
            var clientWidth = selectedDomInfo.ClientWidth;

            if (rtl)
            {
                var offsetCentered = contentWidth - offsetLeft - clientWidth / 2 - wrapperWidth / 2;
                return -Math.Min(contentWidth - wrapperWidth, Math.Max(0, offsetCentered));
            }
            else
            {
                var offsetCentered = offsetLeft + clientWidth / 2 - wrapperWidth / 2;
                return Math.Min(contentWidth - wrapperWidth, Math.Max(0, offsetCentered));
            }
        }

        protected async Task ScrollTo(string direction)
        {
            (WrapperWidth, ContentWidth) = await GetWidths();
            ScrollOffset = CalculateNewOffset(direction, WrapperWidth, ContentWidth, Rtl, ScrollOffset);
        }

        protected static double CalculateNewOffset(string direction, double wrapperWidth, double contentWidth, bool rtl, double currentScrollOffset)
        {
            var sign = rtl ? -1 : 1;

            var newAbsoluteOffset = sign * currentScrollOffset + (direction == "prev" ? -1 : 1) * wrapperWidth;

            return sign * Math.Max(Math.Min(newAbsoluteOffset, contentWidth - wrapperWidth), 0);
        }
    }
}