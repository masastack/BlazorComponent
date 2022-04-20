﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public class BActivatable : BToggleable, IActivatable, IHandleEvent
    {
        private string _activatorId;

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
        public bool OpenOnFocus
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        [Parameter]
        public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

        protected bool IsBooted { get; set; }

        protected Dictionary<string, object> ActivatorEvents { get; set; } = new();

        public virtual Dictionary<string, object> ActivatorAttributes => new(ActivatorEvents)
        {
            { ActivatorId, true },
            { "role", "button" },
            { "aria-haspopup", true },
            { "aria-expanded", IsActive }
        };

        protected string ActivatorId => _activatorId ??= $"_activator_{Guid.NewGuid()}";

        protected string ActivatorSelector => $"[{ActivatorId}]";

        protected RenderFragment ComputedActivatorContent
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

        RenderFragment IActivatable.ComputedActivatorContent => ComputedActivatorContent;

        async Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem item, object? arg)
        {
            await item.InvokeAsync(arg);
        }

        protected override void OnWatcherInitialized()
        {
            base.OnWatcherInitialized();

            Watcher
                .Watch<bool>(nameof(Disabled), val => { ResetActivatorEvents(); })
                // .Watch<bool>(nameof(Value), OnValueChanged)
                .Watch<bool>(nameof(OpenOnFocus), () => { ResetActivatorEvents(); })
                .Watch<bool>(nameof(OpenOnHover), () => { ResetActivatorEvents(); });
        }

        protected override async void OnValueChanged(bool value)
        {
            if (_isFromOnActiveUpdated)
            {
                _isFromOnActiveUpdated = false;
                return;
            }

            if (!IsBooted)
            {
                NextTick(() => SetActive(value));
            }
            else
            {
                await SetActive(value);
            }
        }

        private void ResetActivatorEvents()
        {
            ActivatorEvents.Clear();
            AddActivatorEvents();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            ResetActivatorEvents();
        }

        private void AddActivatorEvents()
        {
            if (Disabled)
            {
                return;
            }

            if (OpenOnHover)
            {
                ActivatorEvents.Add("onmouseenter", CreateEventCallback<MouseEventArgs>(HandleOnMouseEnterAsync));
                ActivatorEvents.Add("onmouseleave", CreateEventCallback<MouseEventArgs>(HandleOnMouseLeaveAsync));
            }
            else
            {
                ActivatorEvents.Add("onexclick", CreateEventCallback<MouseEventArgs>(HandleOnClickAsync));
                ActivatorEvents.Add("__internal_stopPropagation_onexclick", true);
            }

            if (OpenOnFocus)
            {
                ActivatorEvents.Add("onfocus", CreateEventCallback<FocusEventArgs>(HandleOnFocusAsync));
                ActivatorEvents.Add("onblur", CreateEventCallback<FocusEventArgs>(HandleOnBlurAsync));
            }
        }

        private async Task HandleOnMouseEnterAsync(MouseEventArgs args)
        {
            await RunOpenDelayAsync();
        }

        private bool _isFromOnActiveUpdated;

        protected override async Task OnActiveUpdated(bool value)
        {
            if (value != Value && ValueChanged.HasDelegate)
            {
                _isFromOnActiveUpdated = true;
                await ValueChanged.InvokeAsync(value);
            }
        }

        private async Task HandleOnMouseLeaveAsync(MouseEventArgs args)
        {
            await RunCloseDelayAsync();
        }

        protected virtual async Task HandleOnClickAsync(MouseEventArgs args)
        {
            // TODO: focus

            await RunOpenDelayAsync();
        }

        private async Task HandleOnFocusAsync(FocusEventArgs args)
        {
            await RunOpenDelayAsync();
        }

        private async Task HandleOnBlurAsync(FocusEventArgs args)
        {
            await RunCloseDelayAsync();
        }
    }
}