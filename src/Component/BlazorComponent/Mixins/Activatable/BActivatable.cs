using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public abstract class BActivatable : BDelayable, IActivatable
{
    private readonly string _activatorId;

    private bool _isActive;

    private ElementReference? _externalActivatorRef;

    private Dictionary<string, EventCallback<FocusEventArgs>> _focusListeners = new();
    private Dictionary<string, EventCallback<KeyboardEventArgs>> _keyboardListeners = new();
    private Dictionary<string, (EventCallback<MouseEventArgs> listener, EventListenerActions actions)> _mouseListeners = new();

    protected BActivatable()
    {
        _activatorId = $"_activator_{Guid.NewGuid()}";
    }

    private string InternalActivatorSelector => $"[{_activatorId}]";

    protected string ActivatorSelector => _externalActivatorRef.HasValue
        ? Document.QuerySelector(_externalActivatorRef.Value).Selector
        : InternalActivatorSelector;

    protected HtmlElement ActivatorElement { get; private set; }

    protected virtual bool IsActive
    {
        get => _isActive;
        set
        {
            if (Disabled) return;

            _isActive = value;
        }
    }

    protected bool HasActivator => ActivatorContent != null || _externalActivatorRef != null;

    public RenderFragment ComputedActivatorContent
    {
        get
        {
            if (ActivatorContent != null)
            {
                var props = new ActivatorProps(GenActivatorAttributes());
                return ActivatorContent(props);
            }

            return null;
        }
    }

    [Inject]
    public Document Document { get; set; }

    [Parameter]
    public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

    [Parameter]
    public bool Disabled
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter]
    public bool OpenOnHover { get; set; }

    [Parameter]
    public bool OpenOnFocus { get; set; }

    [Parameter]
    public virtual bool Value
    {
        get => IsActive;
        set => IsActive = value;
    }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Watcher
            .Watch<bool>(nameof(Disabled), val =>
            {
                _ = ResetActivator();
            });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ResetActivator();
        }
    }

    protected virtual async Task Open()
    {
        await RunOpenDelay(() => UpdateValue(true));
    }

    protected virtual async Task Close()
    {
        await RunCloseDelay(() => UpdateValue(false));
    }

    protected virtual Task Toggle()
    {
        return UpdateValue(!Value);
    }

    protected async Task UpdateValue(bool value)
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
        else
        {
            Value = value;
        }
    }

    private async Task AddActivatorEvents()
    {
        if (Disabled || GetActivator() == null) return;

        _focusListeners = GenActivatorFocusListeners();

        _mouseListeners = GenActivatorMouseListeners();

        _keyboardListeners = GenActivatorKeyboardListeners();

        foreach (var (key, (listener, actions)) in _mouseListeners)
        {
            await ActivatorElement.AddEventListenerAsync(key, listener, false, actions);
        }

        foreach (var (key, value) in _focusListeners)
        {
            await ActivatorElement.AddEventListenerAsync(key, value, false);
        }

        foreach (var (key, value) in _keyboardListeners)
        {
            await ActivatorElement.AddEventListenerAsync(key, value, false);
        }
    }

    private Dictionary<string, object> GenActivatorAttributes()
    {
        return new Dictionary<string, object>
        {
            {_activatorId, true},
            {"role", "button"},
            {"aria-haspopup", true},
            {"aria-expanded", IsActive}
        };
    }

    protected virtual Dictionary<string, (EventCallback<MouseEventArgs> listener, EventListenerActions actions)> GenActivatorMouseListeners()
    {
        Dictionary<string, (EventCallback<MouseEventArgs>, EventListenerActions)> listeners = new();

        if (Disabled) return listeners;

        if (OpenOnHover)
        {
            listeners.Add("mouseenter", (CreateEventCallback<MouseEventArgs>(_ => Open()), null));

            listeners.Add("mouseleave", (CreateEventCallback<MouseEventArgs>(_ => Close()), null));
        }
        else
        {
            listeners.Add("click", (CreateEventCallback<MouseEventArgs>(async _ =>
            {
                await JsInvokeAsync(JsInteropConstants.Focus, ActivatorSelector);
                await Toggle();
            }), new EventListenerActions(true)));
        }

        return listeners;
    }

    protected virtual Dictionary<string, EventCallback<FocusEventArgs>> GenActivatorFocusListeners()
    {
        Dictionary<string, EventCallback<FocusEventArgs>> listeners = new();

        if (Disabled || !OpenOnFocus) return listeners;

        listeners.Add("focus", CreateEventCallback<FocusEventArgs>(_ => Open()));

        listeners.Add("blur", CreateEventCallback<FocusEventArgs>(_ => Close()));

        return listeners;
    }

    protected virtual Dictionary<string, EventCallback<KeyboardEventArgs>> GenActivatorKeyboardListeners()
    {
        Dictionary<string, EventCallback<KeyboardEventArgs>> listeners = new();

        if (Disabled) return listeners;

        listeners.Add("keydown", CreateEventCallback<KeyboardEventArgs>(async args =>
        {
            if (args.Key == "Escape")
            {
                await Close();
            }
        }));

        return listeners;
    }

    protected HtmlElement GetActivator()
    {
        if (ActivatorElement != null) return ActivatorElement;

        if (ActivatorContent != null)
        {
            ActivatorElement = Document.QuerySelector(InternalActivatorSelector);
        }
        else if (_externalActivatorRef != null)
        {
            ActivatorElement = Document.QuerySelector(_externalActivatorRef.Value);
        }

        return ActivatorElement;
    }

    private async Task RemoveActivatorEvents()
    {
        if (ActivatorElement == null) return;

        foreach (var (key, _) in _mouseListeners)
        {
            await ActivatorElement.RemoveEventListenerAsync(key);
        }

        foreach (var (key, _) in _focusListeners)
        {
            await ActivatorElement.RemoveEventListenerAsync(key);
        }

        foreach (var (key, _) in _keyboardListeners)
        {
            await ActivatorElement.RemoveEventListenerAsync(key);
        }

        _mouseListeners.Clear();
        _focusListeners.Clear();
        _keyboardListeners.Clear();
    }

    public async Task UpdateActivator(ElementReference el)
    {
        _externalActivatorRef = el;
        await ResetActivator();
    }

    private async Task ResetActivator()
    {
        await RemoveActivatorEvents();
        ActivatorElement = null;
        GetActivator();
        await AddActivatorEvents();
    }
}