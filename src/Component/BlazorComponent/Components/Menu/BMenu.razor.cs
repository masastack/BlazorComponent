using BlazorComponent.JSInterop;
using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent;

public partial class BMenu : BMenuable, IDependent
{
    [Inject]
    private OutsideClickJSModule? Module { get; set; }

    [CascadingParameter]
    public IDependent? CascadingDependent { get; set; }

    [CascadingParameter(Name = "AppIsDark")]
    public bool AppIsDark { get; set; }

    [Parameter]
    public bool Auto { get; set; }

    [Parameter]
    public bool CloseOnClick { get; set; } = true;

    [Parameter]
    public bool CloseOnContentClick
    {
        get => GetValue<bool>();
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

    private bool _isPopupEventsRegistered;

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

    public virtual IEnumerable<string> DependentSelectors
    {
        get
        {
            var elements = _dependents
                           .SelectMany(dependent => dependent.DependentSelectors)
                           .ToList();

            elements.Add(ActivatorSelector);

            // do not use the ContentElement elementReference because it's delay assignment.
            elements.Add($"#{Id}");

            return elements;
        }
    }

    private readonly List<IDependent> _dependents = new();

    protected int DefaultOffset { get; set; } = 8;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (CascadingDependent is not null)
        {
            (this as IDependent).CascadingDependents.ForEach(item => item.RegisterChild(this));
        }
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        // BUG: defaultValue is required, because there is a bug about Watcher
        // default value would not works if GetValue(defaultValue) int CloseOnContentClick's getter never be called,
        // so there need to assign the default value at Watch method.
        watcher.Watch(nameof(CloseOnContentClick), () => ResetPopupEvents(CloseOnContentClick), defaultValue: true);
    }

    public void RegisterChild(IDependent dependent)
    {
        _dependents.Add(dependent);
        Module?.UpdateDependentElements(DependentSelectors.ToArray());
    }

    //TODO:keydown event

    protected override async Task WhenIsActiveUpdating(bool value)
    {
        await base.WhenIsActiveUpdating(value);

        if (!_isPopupEventsRegistered && ContentElement.Context is not null)
        {
            _isPopupEventsRegistered = true;

            RegisterPopupEvents(ContentElement.GetSelector(), CloseOnContentClick);
        }

        if (!OpenOnHover && CloseOnClick && Module is { Initialized: false })
        {
            await Module.InitializeAsync(this, DependentSelectors.ToArray());
        }
    }

    private Func<ValueTask<bool>>? CloseConditional { get; set; }

    private Func<Task>? Handler { get; set; }

    public override async Task HandleOnOutsideClickAsync()
    {
        if (IsActive && CloseOnClick)
        {
            await OnOutsideClick.InvokeAsync();
            RunDirectly(false);
        }
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
