using BlazorComponent.JSInterop;
using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent;

public partial class BSpeedDial : BBootable
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string Direction { get; set; } = "top";

    [Parameter]
    public bool Top { get; set; }

    [Parameter]
    public bool Right { get; set; }

    [Parameter]
    public bool Bottom { get; set; }

    [Parameter]
    public bool Left { get; set; }

    [Parameter]
    public bool Fixed { get; set; }

    [Parameter]
    public bool Absolute { get; set; }

    [Parameter]
    public string Transition { get; set; } = "scale-transition";

    [Parameter]
    public string? Origin { get; set; }

    private string Tag { get; set; } = "div";

    private OutsideClickJSModule? _outsideClickJsModule;

    protected ElementReference ContentElement { get; set; }

    protected override void OnWatcherInitialized()
    {
        base.OnWatcherInitialized();

        Watcher.Watch<bool>(nameof(OpenOnHover),
            () => ResetPopupEvents(true));
    }

    protected override async Task WhenIsActiveUpdating(bool value)
    {
        await base.WhenIsActiveUpdating(value);

        if (_outsideClickJsModule is null)
        {
            _outsideClickJsModule = new OutsideClickJSModule(this, Js);
            await _outsideClickJsModule.InitializeAsync(ActivatorSelector, ContentElement.GetSelector());

            RegisterPopupEvents(ContentElement.GetSelector(), true);
        }
    }

    private Dictionary<string, object> ContentAttributes => new(Attributes) { { "close-condition", IsActive } };

    public override Task HandleOnOutsideClickAsync()
    {
        if (IsActive)
        {
            RunDirectly(false);
        }

        return Task.CompletedTask;
    }
}
