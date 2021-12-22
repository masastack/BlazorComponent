using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using BlazorComponent.Mixins;

namespace BlazorComponent
{
    public partial class BMenu : BMenuable, IDependent
    {
        private readonly int _defaultOffset = 8;
        private readonly List<IDependent> _dependents = new();

        protected string CalculatedLeft
        {
            get
            {
                var menuWidth = Math.Max(Dimensions.content.Width, NumberHelper.ParseDouble(CalculatedMinWidth));

                if (!Auto) return CalcLeft(menuWidth) ?? "0";

                return ((StringNumber)CalcXOverflow(CalcLeftAuto(), menuWidth)).ConvertToUnit() ?? "0";
            }
        }

        protected string CalculatedMaxHeight => Auto ? "200px" : MaxHeight.ConvertToUnit();

        protected string CalculatedMaxWidth => MaxWidth.ConvertToUnit();

        protected string CalculatedMinWidth
        {
            get
            {
                if (MinWidth != null)
                {
                    return MinWidth.ConvertToUnit();
                }

                var nudgeWidth = 0d;
                if (NudgeWidth != null)
                {
                    (_, nudgeWidth) = NudgeWidth.TryGetNumber();
                }

                var minWidth = Math.Min(
                    Dimensions.activator.Width + nudgeWidth + (Auto ? 16 : 0),
                    Math.Max(PageWidth - 24, 0));

                double calculatedMaxWidth;
                if (NumberHelper.TryParseDouble(CalculatedMaxWidth, out var value))
                {
                    calculatedMaxWidth = value;
                }
                else
                {
                    calculatedMaxWidth = minWidth;
                }

                return ((StringNumber)Math.Min(calculatedMaxWidth, minWidth)).ConvertToUnit();
            }
        }

        protected string CalculatedTop => !Auto ? CalcTop() : ((StringNumber)CalcYOverflow(CalcTopAuto())).ConvertToUnit();

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public bool Auto { get; set; }

        [Parameter]
        public bool CloseOnClick { get; set; } = true;

        [Parameter]
        public bool CloseOnContentClick { get; set; } = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string ContentClass { get; set; }

        [Parameter]
        public string ContentStyle { get; set; }

        [Parameter]
        public bool DisableKeys { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; } = "auto";

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public string Origin { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string Transition { get; set; }

        [CascadingParameter]
        public IDependent CascadingDependent { get; set; }

        public IEnumerable<HtmlElement> DependentElements
        {
            get
            {
                var elements = _dependents
                    .SelectMany(dependent => dependent.DependentElements)
                    .ToList();

                var contentElement = Document.GetElementById(Id);
                elements.Add(contentElement);

                return elements;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (CascadingDependent != null)
            {
                CascadingDependent.RegisterChild(this);
            }
        }

        public void RegisterChild(IDependent dependent)
        {
            _dependents.Add(dependent);
        }

        protected override async Task AfterShowContent()
        {
            if (!OpenOnHover)
            {
                await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                    DotNetObjectReference.Create(new Invoker<object>(OutsideClick)),
                    new[] { Document.GetElementByReference(ContentRef).Selector, ActivatorSelector });
            }
            else
            {
                await ActivatorElement.AddEventListenerAsync(
                    "mouseleave",
                    CreateEventCallback<MouseEventArgs>(_ => Close()),
                    false,
                    new EventListenerActions(Document.GetElementByReference(ContentRef).Selector));
            }
        }

        protected override Task Activate(Action lazySetter)
        {
            return UpdateDimensions(lazySetter);
        }

        protected override Dictionary<string, (EventCallback<MouseEventArgs>, EventListenerActions)> GenActivatorMouseListeners()
        {
            var listeners = base.GenActivatorMouseListeners();

            // if contentRef is null, remove the mouseleave listener.
            // the mouseleave event will be listened again in AfterShowContent method.
            if (listeners.ContainsKey("mouseleave") && ContentRef.Context == null)
            {
                listeners.Remove("mouseleave");
            }

            return listeners;
        }

        protected override Dictionary<string, EventCallback<KeyboardEventArgs>> GenActivatorKeyboardListeners()
        {
            var listeners = base.GenActivatorKeyboardListeners();

            if (DisableKeys && listeners.ContainsKey("keydown"))
            {
                listeners.Remove("keydown");
            }

            return listeners;
        }

        protected virtual async void OnContentClick(MouseEventArgs args)
        {
            // TODO: if clicked element attribute contains 'disabled', return
            if (CloseOnContentClick)
            {
                await UpdateValue(false);
            }
            else
            {
                PreventRender();
            }
        }

        protected virtual async void OnContentMouseenter(MouseEventArgs args)
        {
            if (!Disabled && OpenOnHover)
            {
                if (Value) await Task.CompletedTask;

                await UpdateValue(true);
            }
            else
            {
                PreventRender();
            }
        }

        protected virtual async void OnContentMouseleave(MouseEventArgs args)
        {
            if (OpenOnHover)
            {
                await UpdateValue(false);
            }
            else
            {
                PreventRender();
            }
        }

        private double CalcTopAuto()
        {
            if (OffsetY)
            {
                return ComputedTop;
            }

            // ignores some code about List

            return ComputedTop - 1;
        }

        private double CalcLeftAuto()
        {
            return Dimensions.activator.Left - _defaultOffset * 2;
        }

        private async Task OutsideClick(object _)
        {
            if (Value)
            {
                await OnOutsideClick.InvokeAsync();
            }

            if (CloseOnClick)
            {
                await UpdateValue(false);
            }

            await InvokeStateHasChangedAsync();
        }

        protected override Task MoveContentTo()
        {
            return Task.CompletedTask;
        }
    }
}