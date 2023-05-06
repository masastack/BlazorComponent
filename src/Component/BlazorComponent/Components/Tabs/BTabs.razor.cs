namespace BlazorComponent
{
    public partial class BTabs : BDomComponentBase, ITabs, IAncestorRoutable
    {
        [Inject]
        private DomEventJsInterop? DomEventJsInterop { get; set; }

        [CascadingParameter(Name = "rtl")]
        public bool Rtl { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        [Parameter]
        public virtual bool HideSlider { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Optional { get; set; }

        [Parameter]
        public string? SliderColor { get; set; }

        [Parameter]
        [ApiDefaultValue(2)]
        public StringNumber SliderSize { get; set; } = 2;

        [Parameter]
        public StringNumber? Value { get; set; }

        private EventCallback<StringNumber>? _valueChanged;

        [Parameter]
        public EventCallback<StringNumber> ValueChanged
        {
            get
            {
                if (_valueChanged.HasValue)
                {
                    return _valueChanged.Value;
                }

                return EventCallback.Factory.Create<StringNumber>(this, (v) => Value = v);
            }
            set => _valueChanged = value;
        }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public string? NextIcon { get; set; }

        [Parameter]
        public string? PrevIcon { get; set; }

        [Parameter]
        public StringBoolean? ShowArrows { get; set; }

        [Parameter]
        public bool Routable { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        private StringNumber? _prevValue;
        private int _registeredTabItemsIndex = 0;

        private List<ITabItem> TabItems { get; set; } = new();

        private object TabsBarRef { get; set; }

        protected(StringNumber height, StringNumber left, StringNumber right, StringNumber top, StringNumber width) Slider { get; set; }

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

        List<ITabItem> ITabs.TabItems => TabItems;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                DomEventJsInterop?.ResizeObserver(Ref.GetSelector(), OnResize);

                await CallSlider();
            }
            else if (_prevValue != Value)
            {
                _prevValue = Value;
                await CallSlider();
            }
        }

        public bool IsReversed => Rtl && Vertical;

        public BSlideGroup Instance => TabsBarRef as BSlideGroup;

        public void RegisterTabItem(ITabItem tabItem)
        {
            tabItem.Value ??= _registeredTabItemsIndex++;

            if (TabItems.Any(item => item.Value.Equals(tabItem.Value))) return;

            TabItems.Add(tabItem);
        }

        public void UnregisterTabItem(ITabItem tabItem)
        {
            TabItems.Remove(tabItem);
        }

        public async Task CallSlider()
        {
            if (HideSlider) return;

            var item = Instance?.Items?.FirstOrDefault(item => item.Value == Instance.Value);
            if (item?.Ref.Context == null)
            {
                Slider = (0, 0, 0, 0, 0);
            }
            else
            {
                var el = await JsInvokeAsync<Web.Element>(JsInteropConstants.GetDomInfo, item.Ref);
                var height = !Vertical ? SliderSize.TryGetNumber().number : el.ScrollHeight;
                var left = Vertical ? 0 : el.OffsetLeft;
                var right = Vertical ? 0 : el.OffsetLeft + el.OffsetWidth;
                var top = el.OffsetTop;
                var width = Vertical ? SliderSize.TryGetNumber().number : el.ScrollWidth;

                Slider = (height, left, right, top, width);
            }

            StateHasChanged();
        }

        private async Task OnResize()
        {
            if (IsDisposed)
            {
                return;
            }

            await CallSlider();
        }
    }
}
