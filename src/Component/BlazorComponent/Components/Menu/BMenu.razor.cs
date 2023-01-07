using BlazorComponent.JSInterop;
using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

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
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? ContentStyle { get; set; }

        [Parameter]
        public bool DisableKeys { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; } = "auto";

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public string? Origin { get; set; }

        [Parameter]
        public StringBoolean? Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public string? Transition { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IDependent? CascadingDependent { get; set; }

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

        protected StringNumber? CalculatedLeft
        {
            get
            {
                var menuWidth = Math.Max(Dimensions.Content.Width, NumberHelper.ParseDouble(CalculatedMinWidth));

                if (!Auto) return CalcLeft(menuWidth);

                return CalcXOverflow(CalcLeftAuto(), menuWidth);
            }
        }

        protected string CalculatedMaxHeight => Auto ? "200px" : MaxHeight.ConvertToUnit();

        protected string CalculatedMaxWidth => MaxWidth?.ConvertToUnit() ?? "";

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

        protected StringNumber? CalculatedTop => !Auto ? CalcTop() : CalcYOverflow(CalcTopAuto());

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

        protected override async Task WhenIsActiveUpdating(bool value)
        {
            if (CloseOnClick && !OpenOnHover && !Attached)
            {
                // in the Select component, outside-click events for activator are registered.
                // In order to ensure that only one event per component is registered,
                // need to delete all registered outside-click events
                // and then add new outside-click event.
                await RemoveOutsideClickEventListener();

                await AddOutsideClickEventListener();
            }

            await base.WhenIsActiveUpdating(value);
        }

        public async Task AddOutsideClickEventListener()
        {
            var noInvokeSelectors = DependentElements.Select(s => s.Selector ?? "").Concat(new[] { ActivatorSelector });

            await Js.AddOutsideClickEventListener(HandleOutsideClickAsync, noInvokeSelectors);
        }

        public async Task RemoveOutsideClickEventListener()
        {
            string[] activatorSelectors = { ActivatorSelector };
            await JsInvokeAsync(JsInteropConstants.RemoveOutsideClickEventListener, (object)activatorSelectors);

            string[] noInvokeSelectors = { ContentElement.GetSelector(), ActivatorSelector };
            await JsInvokeAsync(JsInteropConstants.RemoveOutsideClickEventListener, (object)noInvokeSelectors);
        }

        public Func<ClickOutsideArgs, Task<bool>>? CloseConditional { get; set; }

        public Func<Task>? Handler { get; set; }

        private async Task HandleOutsideClickAsync(ClickOutsideArgs args)
        {
            CloseConditional ??= _ => Task.FromResult(IsActive && CloseOnClick);

            Handler ??= async () =>
            {
                await OnOutsideClick.InvokeAsync();
                RunDirectly(false);
            };

            if (!await CloseConditional!(args)) return;

            await Handler();
        }

        protected async Task HandleOnContentClickAsync(MouseEventArgs _)
        {
            RunDirectly(false);
        }

        protected async Task HandleOnContentMouseenterAsync(MouseEventArgs args)
        {
            RunDelaying(true);
        }

        protected async Task HandleOnContentMouseleaveAsync(MouseEventArgs args)
        {
            RunDelaying(false);
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
