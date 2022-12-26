using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTabs : BDomComponentBase, ITabs
    {
        [CascadingParameter(Name = "rtl")]
        public bool Rtl { get; set; }

        [Parameter]
        public virtual bool HideSlider { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Optional { get; set; }

        [Parameter]
        public string SliderColor { get; set; }

        [Parameter]
        public StringNumber SliderSize { get; set; } = 2;

        [Parameter]
        public StringNumberOrMore Value { get; set; }

        private EventCallback<StringNumberOrMore>? _valueChanged;

        [Parameter]
        public EventCallback<StringNumberOrMore> ValueChanged
        {
            get
            {
                if (_valueChanged.HasValue)
                {
                    return _valueChanged.Value;
                }

                return EventCallback.Factory.Create<StringNumberOrMore>(this, (v) => Value = v);
            }
            set => _valueChanged = value;
        }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public StringBoolean ShowArrows { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        private StringNumber _prevValue;

        private List<ITabItem> TabItems { get; set; }

        private object TabsBarRef { get; set; }

        protected (StringNumber height, StringNumber left, StringNumber right, StringNumber top, StringNumber width) Slider { get; set; }

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
            if (firstRender)
            {
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
            TabItems ??= new List<ITabItem>();

            tabItem.Value ??= TabItems.Count;

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
    }
}