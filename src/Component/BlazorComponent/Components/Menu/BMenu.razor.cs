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
        public bool CloseOnClick
        {
            get => GetValue(true);
            set => SetValue(value);
        }

        [Parameter]
        public bool CloseOnContentClick
        {
            get => GetValue(true);
            set => SetValue(value);
        }

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

        protected string? CalculatedMaxHeight => Auto ? "200px" : MaxHeight.ConvertToUnit();

        protected string CalculatedMaxWidth => MaxWidth?.ConvertToUnit() ?? "";

        protected string? CalculatedMinWidth
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

        protected List<IDependent> Dependents { get; } = new();

        public bool DisableDefaultOutsideClickEvent { get; set; }

        protected int DefaultOffset { get; set; } = 8;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (CascadingDependent != null)
            {
                CascadingDependent.RegisterChild(this);
            }
        }

        protected override void OnWatcherInitialized()
        {
            base.OnWatcherInitialized();

            Watcher.Watch<bool>(nameof(CloseOnClick), ResetPopupEvents)
                   .Watch<bool>(nameof(CloseOnContentClick), ResetPopupEvents);
        }

        public void RegisterChild(IDependent dependent)
        {
            Dependents.Add(dependent);
        }

        private void ResetPopupEvents()
        {
            ResetPopupEvents(CloseOnClick, CloseOnContentClick);
        }

        //TODO:keydown event

        private bool _isPopupEventsRegistered;

        protected override async Task WhenIsActiveUpdating(bool value)
        {
            if (!_isPopupEventsRegistered && ContentElement.Context is not null)
            {
                RegisterPopupEvents();
                _isPopupEventsRegistered = true;
            }

            await base.WhenIsActiveUpdating(value);
        }

        public void RegisterPopupEvents()
        {
            RegisterPopupEvents(ContentElement.GetSelector(), CloseOnClick, CloseOnContentClick, DisableDefaultOutsideClickEvent);
        }

        public Func<ValueTask<bool>>? CloseConditional { get; set; }

        public Func<Task>? Handler { get; set; }

        public override async Task HandleOnOutsideClickAsync()
        {
            CloseConditional ??= () => new ValueTask<bool>(IsActive && CloseOnClick);

            Handler ??= async () =>
            {
                await OnOutsideClick.InvokeAsync();
                RunDirectly(false);
            };

            if (!await CloseConditional!()) return;

            await Handler();
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
