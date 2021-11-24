using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTabs : BDomComponentBase, ITabs
    {
        private bool _isModified;
        private StringNumber _value;

        private List<ITabItem> TabItems { get; set; }

        private AbstractComponent TabsBarRef { get; set; }

        protected(StringNumber height, StringNumber left, StringNumber right, StringNumber top, StringNumber width) Slider { get; set; }

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
        public StringNumber Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _isModified = true;
                }

                _value = value;
            }
        }

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
        public string NextIcon { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public StringBoolean ShowArrows { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await CallSlider(firstRender);
        }

        public bool IsReversed => Rtl && Vertical;

        public BSlideGroup Instance => TabsBarRef?.Instance as BSlideGroup;

        public void RegisterTabItem(ITabItem tabItem)
        {
            TabItems ??= new List<ITabItem>();

            tabItem.Value ??= TabItems.Count;

            if (TabItems.Any(item => item.Value.Equals(tabItem.Value))) return;

            TabItems.Add(tabItem);

            StateHasChanged();
        }

        public void UnregisterTabItem(ITabItem tabItem)
        {
            TabItems.Remove(tabItem);
        }

        public async Task CallSlider(bool firstRender = false)
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

            if (firstRender || _isModified)
            {
                StateHasChanged();

                _isModified = false;
            }
        }
    }
}