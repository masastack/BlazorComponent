using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public abstract class BActivatable : BDomComponentBase, IActivatable
{
    private readonly string _activatorId;

    private bool _isActive;

    private HtmlElement _activatorElement;
    private ElementReference? _externalActivatorRef;

    private RenderFragment<ActivatorProps> _activatorContent;
    private bool _activatorContentChanged;

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

    [Inject]
    public Document Document { get; set; }

    [Parameter]
    public RenderFragment<ActivatorProps> ActivatorContent
    {
        get => _activatorContent;
        set
        {
            _activatorContent = value;
            _activatorContentChanged = true;
        }
    }

    [Parameter]
    public bool Disabled { get; set; }

    [Parameter]
    public bool OpenOnHover { get; set; }

    [Parameter]
    public bool OpenOnFocus { get; set; }

    [Parameter]
    public virtual bool Value
    {
        get => IsActive;
        set
        {
            if (value == IsActive) return;
            
            IsActive = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender || _activatorContentChanged)
        {
            _activatorContentChanged = false;
            await ResetActivator();
        }
    }

    protected abstract Task Open();

    protected abstract Task Close();

    protected abstract Task Toggle();

    private async Task AddActivatorEvents()
    {
        if (Disabled || GetActivator() == null) return;

        _mouseListeners = GenActivatorMouseListeners();

        _focusListeners = GenActivatorFocusListeners();

        _keyboardListeners = GenActivatorKeyboardListeners();

        foreach (var (key, (listener, actions)) in _mouseListeners)
        {
            await _activatorElement.AddEventListenerAsync(key, listener, false, actions);
        }

        foreach (var (key, value) in _focusListeners)
        {
            await _activatorElement.AddEventListenerAsync(key, value, false);
        }

        foreach (var (key, value) in _keyboardListeners)
        {
            await _activatorElement.AddEventListenerAsync(key, value, false);
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
            listeners.Add("mouseenter", (CreateEventCallback<MouseEventArgs>(async _ => await Open()), null));

            listeners.Add("mouseleave", (CreateEventCallback<MouseEventArgs>(async _ => await Close()), null));
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

        listeners.Add("focus", CreateEventCallback<FocusEventArgs>(async _ => await Open()));

        listeners.Add("blur", CreateEventCallback<FocusEventArgs>(async _ => await Close()));

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
        if (_activatorElement != null) return _activatorElement;

        if (ActivatorContent != null)
        {
            _activatorElement = Document.QuerySelector(InternalActivatorSelector);
        }
        else if (_externalActivatorRef != null)
        {
            _activatorElement = Document.QuerySelector(_externalActivatorRef.Value);
        }

        return _activatorElement;
    }

    private async Task RemoveActivatorEvents()
    {
        if (_activatorElement == null) return;

        foreach (var (key, _) in _mouseListeners)
        {
            await _activatorElement.RemoveEventListenerAsync(key);
        }

        foreach (var (key, _) in _focusListeners)
        {
            await _activatorElement.RemoveEventListenerAsync(key);
        }

        foreach (var (key, _) in _keyboardListeners)
        {
            await _activatorElement.RemoveEventListenerAsync(key);
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
        _activatorElement = null;
        GetActivator();
        await AddActivatorEvents();
    }
}