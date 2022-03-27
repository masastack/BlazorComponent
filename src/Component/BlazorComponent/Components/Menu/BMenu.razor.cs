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
        [Parameter]
        public bool Auto { get; set; }

        [Parameter]
        public bool CloseOnClick { get; set; } = true;

        [Parameter]
        public bool CloseOnContentClick { get; set; } = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string ContentStyle { get; set; }

        //TODO:fix here
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

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IDependent CascadingDependent { get; set; }

        [CascadingParameter(Name = "AppIsDark")]
        public bool AppIsDark { get; set; }

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

                return AppIsDark;
            }
        }

        protected StringNumber CalculatedLeft
        {
            get
            {
                var menuWidth = Math.Max(Dimensions.Content.Width, NumberHelper.ParseDouble(CalculatedMinWidth));

                if (!Auto) return CalcLeft(menuWidth);

                return CalcXOverflow(CalcLeftAuto(), menuWidth);
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
                    Dimensions.Activator.Width + nudgeWidth + (Auto ? 16 : 0),
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

        protected StringNumber CalculatedTop => !Auto ? CalcTop() : CalcYOverflow(CalcTopAuto());

        public IEnumerable<HtmlElement> DependentElements
        {
            get
            {
                var elements = Dependents
                               .SelectMany(dependent => dependent.DependentElements)
                               .ToList();

                var contentElement = Document.GetElementById(Id);
                elements.Add(contentElement);

                return elements;
            }
        }

        protected Dictionary<string, object> ContentAttributes
        {
            get
            {
                var attributes = new Dictionary<string, object>(Attributes);

                if (CloseOnContentClick)
                {
                    attributes.Add("onclick", CreateEventCallback<MouseEventArgs>(HandleOnContentClickAsync));
                }

                if (!Disabled && OpenOnHover)
                {
                    attributes.Add("onmouseenter", CreateEventCallback<MouseEventArgs>(HandleOnContentMouseenterAsync));
                }

                if (OpenOnHover)
                {
                    attributes.Add("onmouseleave", CreateEventCallback<MouseEventArgs>(HandleOnContentMouseleaveAsync));
                }

                attributes.Add("close-condition", IsActive && CloseOnClick);

                return attributes;
            }
        }

        protected List<IDependent> Dependents { get; } = new();

        protected int DefaultOffset { get; set; } = 8;

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
            Dependents.Add(dependent);
        }

        //TODO:keydown event

        protected override async Task OnActiveUpdated(bool value)
        {
            if (CloseOnClick && !OpenOnHover && !Attached)
            {
                await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                    DotNetObjectReference.Create(new Invoker<object>(HandleOutsideClickAsync)),
                    new[] { Document.GetElementByReference(ContentElement).Selector, ActivatorSelector }, null, ContentElement);
            }

            await base.OnActiveUpdated(value);
        }

        private async Task HandleOutsideClickAsync(object agrs)
        {
            if (!IsActive || !CloseOnClick) return;

            await OnOutsideClick.InvokeAsync();
            await RunCloseDelayAsync();
        }

        protected async Task HandleOnContentClickAsync(MouseEventArgs _)
        {
            await RunCloseDelayAsync();
        }

        protected async Task HandleOnContentMouseenterAsync(MouseEventArgs args)
        {
            await RunOpenDelayAsync();
        }

        protected async Task HandleOnContentMouseleaveAsync(MouseEventArgs args)
        {
            //TODO:If target is activator
            await RunCloseDelayAsync();
        }

        private double CalcTopAuto()
        {
            if (OffsetY)
            {
                return ComputedTop;
            }

            //TODO: check this
            //ignores some code about List

            return ComputedTop - 1;
        }

        private double CalcLeftAuto()
        {
            return Dimensions.Activator.Left - DefaultOffset * 2;
        }
    }
}