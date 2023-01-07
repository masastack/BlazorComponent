using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public class BActivatable : BToggleable, IActivatable, IActivatableJsCallbacks
{
    private string? _activatorId;

    [Parameter]
    public bool Disabled
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool OpenOnHover
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool OpenOnClick { get; set; } = true;

    [Parameter]
    public bool OpenOnFocus
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public RenderFragment<ActivatorProps>? ActivatorContent { get; set; }

    private ActivatableJsInterop? _activatableJsInterop;

    protected bool IsBooted { get; set; }

    private bool HasActivator => ActivatorContent is not null;

    public virtual Dictionary<string, object> ActivatorAttributes => new()
    {
        { ActivatorId, true },
        { "role", "button" },
        { "aria-haspopup", true },
        { "aria-expanded", IsActive }
    };

    private string ActivatorId => _activatorId ??= $"_activator_{Guid.NewGuid()}";

    public string ActivatorSelector => $"[{ActivatorId}]";

    protected RenderFragment? ComputedActivatorContent
    {
        get
        {
            if (ActivatorContent == null)
            {
                return null;
            }

            var props = new ActivatorProps(ActivatorAttributes);
            return ActivatorContent(props);
        }
    }

    bool IActivatable.IsActive => IsActive;

    RenderFragment? IActivatable.ComputedActivatorContent => ComputedActivatorContent;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            if (HasActivator)
            {
                _activatableJsInterop = new ActivatableJsInterop(this, Js);
                await _activatableJsInterop.InitializeAsync();
            }
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (!IsBooted && IsActive)
        {
            IsBooted = true;
        }
    }

    protected override bool AfterHandleEventShouldRender() => false;

    protected override void OnWatcherInitialized()
    {
        base.OnWatcherInitialized();

        Watcher
            .Watch<bool>(nameof(Disabled), ResetActivatorEvents)
            .Watch<bool>(nameof(OpenOnFocus), ResetActivatorEvents)
            .Watch<bool>(nameof(OpenOnHover), ResetActivatorEvents);
    }

    protected override void OnValueChanged(bool value)
    {
        if (!IsBooted)
        {
            NextTick(() => RunDirectly(value));
        }
        else
        {
            RunDirectly(value);
        }
    }

    private void ResetActivatorEvents()
    {
        if (_activatableJsInterop is null) return;

        _ = _activatableJsInterop.ResetEvents();
    }

    public virtual Task HandleOnClickAsync(MouseEventArgs args)
    {
        return Task.CompletedTask;
    }

    public async Task SetActive(bool val)
    {
        await SetActiveInternal(val);
    }

    protected void RunDirectly(bool val)
    {
        _ = _activatableJsInterop is null ? SetActive(val) : _activatableJsInterop.SetActive(val);
    }

    protected void RunDelaying(bool val)
    {
        _activatableJsInterop?.RunDelay(val);
    }
}
