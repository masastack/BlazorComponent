using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorComponent
{
    public partial class BTabs : BDomComponentBase, ITabs
    {
        public AbstractComponent TabsBarRef { get; set; }

        protected(StringNumber height, StringNumber left, StringNumber right, StringNumber top, StringNumber width) Slider { get; set; }

        public List<BTab> Tabs { get; } = new();

        public List<ITabItem> TabItems { get; set; }

        [CascadingParameter(Name = "rtl")]
        public bool Rtl { get; set; }

        [Parameter]
        public virtual bool HideSlider { get; set; }

        [Parameter]
        public string Color { get; set; }

        private RenderFragment _childContent;

        private bool _childContentChanged;

        private readonly Dictionary<int, RenderFragment> _otherSlots = new();

        [Parameter]
        public RenderFragment ChildContent
        {
            get => _childContent;
            set
            {
                _childContent = value;
                _childContentChanged = true;

                var builder = new RenderTreeBuilder();
                value(builder);
                var array = builder.GetFrames().Array;

                if (_otherSlots.Count == 0)
                {
                    var componentOrMarkups = array.Where(u =>
                        u.FrameType == RenderTreeFrameType.Component || (u.FrameType == RenderTreeFrameType.Markup && u.MarkupContent.Trim() != ""));

                    var key = 0;
                    foreach (var item in componentOrMarkups)
                    {
                        var index = array.ToList().IndexOf(item);

                        if (item.FrameType == RenderTreeFrameType.Component &&
                            (item.ComponentType.BaseType == typeof(BTab) || item.ComponentType.FullName.Contains("TabItem")))
                        {
                            key++;
                            continue;
                        }

                        RenderFragment fragment = b =>
                        {
                            bool isComponent = false;
                            foreach (var frame in array.Skip(index))
                            {
                                if (frame.FrameType == RenderTreeFrameType.None) break;

                                if (frame.FrameType == RenderTreeFrameType.Markup)
                                {
                                    b.AddMarkupContent(frame.Sequence, frame.MarkupContent);
                                    break;
                                }

                                if (frame.FrameType == RenderTreeFrameType.Component)
                                {
                                    if (isComponent) break;

                                    b.OpenComponent(frame.Sequence, frame.ComponentType);

                                    isComponent = true;
                                }
                                else
                                {
                                    b.AddAttribute(frame.Sequence, frame.AttributeName, frame.AttributeValue);
                                }
                            }

                            if (item.FrameType == RenderTreeFrameType.Component && isComponent)
                            {
                                b.CloseComponent();
                            }
                        };

                        _otherSlots.Add(key++, fragment);
                    }
                }

                // This setter would be called when using @bind-Value.
                // But it is not expected to be called if at least one tab Value is not assigned.
                // So need to check whether the count on tabs and tab Values are same.

                // The count of tabs in ChildContent
                var countOfTabs = array.Count(frame =>
                    frame.FrameType == RenderTreeFrameType.Component && frame.ComponentType.BaseType.FullName == typeof(BTab).FullName);

                // The list contains all tab Value
                var values = array.Where(frame =>
                        frame.FrameType == RenderTreeFrameType.Attribute &&
                        frame.AttributeName == nameof(Value) &&
                        frame.AttributeValue is StringNumber)
                    .Select(frame => frame.AttributeValue as StringNumber)
                    .ToList();

                // The count of tab Value in ChildContent
                var countOfTabValues = values.Count;

                if (countOfTabs != countOfTabValues) return;

                // Dynamic removing tab
                if (Tabs.Count > values.Count)
                {
                    var removedTabs = Tabs.Select(tab => tab.Value).Except(values).ToList();

                    foreach (var removedTab in removedTabs)
                    {
                        var tabIndex = Tabs.FindIndex(tab => tab.Value == removedTab);
                        Tabs.RemoveAt(tabIndex);

                        if (Instance == null) return;
                        var itemIndex = Instance.Items.FindIndex(item => item.Value == removedTab);
                        Instance.Items.RemoveAt(itemIndex);
                    }
                }
            }
        }

        [Parameter]
        public string SliderColor { get; set; }

        [Parameter]
        public StringNumber SliderSize { get; set; } = 2;

        [Parameter]
        public StringNumber Value { get; set; }

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

            _childContentChanged = false;
        }

        public bool IsReversed => Rtl && Vertical;

        public BSlideGroup Instance => TabsBarRef?.Instance as BSlideGroup;

        public void RegisterTabItem(ITabItem tabItem)
        {
            if (!_childContentChanged) return;

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

            Instance?.SetWidths();

            if (firstRender)
            {
                StateHasChanged();
            }
        }
    }
}