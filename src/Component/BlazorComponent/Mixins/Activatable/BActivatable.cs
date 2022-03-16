using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class BActivatable : BDelayable, IActivatable, IHandleEvent
    {
        private string _activatorId;

        [Parameter]
        public bool Disabled
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool OpenOnHover
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool OpenOnFocus
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool Value
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

        protected bool IsBooted { get; set; }

        protected bool IsActive { get; set; }

        protected Dictionary<string, object> ActivatorEvents { get; set; } = new();

        public virtual Dictionary<string, object> ActivatorAttributes
        {
            get
            {
                return new Dictionary<string, object>(ActivatorEvents)
                {
                    {ActivatorId,true },
                    {"role", "button"},
                    {"aria-haspopup", true},
                    {"aria-expanded", IsActive}
                };
            }
        }

        protected string ActivatorId
        {
            get
            {
                if (_activatorId == null)
                {
                    _activatorId = $"_activator_{Guid.NewGuid()}";
                }

                return _activatorId;
            }
        }

        protected string ActivatorSelector
        {
            get
            {
                return $"[{ActivatorId}]";
            }
        }

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
            Watcher
                .Watch<bool>(nameof(Disabled), val =>
                {
                    ResetActivatorEvents();
                })
                .Watch<bool>(nameof(Value), OnValueChanged)
                .Watch<bool>(nameof(OpenOnFocus), () =>
                {
                    ResetActivatorEvents();
                })
                .Watch<bool>(nameof(OpenOnHover), () =>
                {
                    ResetActivatorEvents();
                });
        }

        protected virtual void OnValueChanged(bool value)
        {
            IsActive = value;
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
                ActivatorEvents.Add("onexmouseenter", CreateEventCallback<MouseEventArgs>(HandleOnMouseEnterAsync));
                ActivatorEvents.Add("onexmouseleave", CreateEventCallback<MouseEventArgs>(HandleOnMouseLeaveAsync));
            }
            else
            {
                ActivatorEvents.Add("onexclick", CreateEventCallback<MouseEventArgs>(HandleOnClickAsync));
                ActivatorEvents.Add("__internal_stopPropagation_onexclick", true);
            }

            if (OpenOnFocus)
            {
                ActivatorEvents.Add("onexfocus", CreateEventCallback<FocusEventArgs>(HandleOnFocusAsync));
            }
        }

        private async Task HandleOnMouseEnterAsync(MouseEventArgs args)
        {
            if (IsActive)
            {
                return;
            }

            await RunOpenDelayAsync(async () =>
            {
                await SetIsActiveAsync(true);
            });
            StateHasChanged();
        }

        protected virtual async Task SetIsActiveAsync(bool isActive)
        {
            if (IsActive == isActive)
            {
                return;
            }

            await OnIsActiveSettingAsync(isActive);
            await OnIsActiveSetAsync(isActive);
        }

        protected virtual Task OnIsActiveSettingAsync(bool isActive)
        {
            return Task.CompletedTask;
        }

        protected virtual async Task OnIsActiveSetAsync(bool isActive)
        {
            IsActive = isActive;
            if (IsActive != Value && ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(IsActive);
            }
            else
            {
                StateHasChanged();
            }
        }

        private async Task HandleOnMouseLeaveAsync(MouseEventArgs args)
        {
            if (!IsActive)
            {
                return;
            }

            await RunCloseDelayAsync(async () =>
            {
                await SetIsActiveAsync(false);
            });
            StateHasChanged();
        }

        protected virtual async Task HandleOnClickAsync(MouseEventArgs args)
        {
            //TODO:focus
            await SetIsActiveAsync(!IsActive);
            StateHasChanged();
        }

        private async Task HandleOnFocusAsync(FocusEventArgs args)
        {
            await SetIsActiveAsync(!IsActive);
            StateHasChanged();
        }
    }
}