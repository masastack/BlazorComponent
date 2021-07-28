using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BlazorComponent
{
    public partial class Transition
    {
        private bool _firstRender = true;
        private bool _visible;
        private bool _value;
        private TransitionState _state;

        protected TransitionContext Context { get; set; } = new();

        protected TransitionState State => _state;

        public Transition()
        {
            Context.CssBuilder
                .AddIf(() => $"{Name}-enter {Name}-enter-active", () => _state == TransitionState.Enter)
                .AddIf(() => $"{Name}-enter-active {Name}-enter-to", () => _state == TransitionState.EnterTo)
                .AddIf(() => $"{Name}-leave {Name}-leave-active", () => _state == TransitionState.Leave)
                .AddIf(() => $"{Name}-leave-active {Name}-leave-to", () => _state == TransitionState.LeaveTo)
                .Add(() => Class);

            Context.StyleBuilder
                .AddIf("display:none", () => !_visible && Mode == TransitionMode.Show)
                .Add(() => Style);
        }

        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Name { get; set; } = "m";

        [Parameter]
        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value)
                {
                    return;
                }

                _value = value;

                if (!_firstRender || Mode == TransitionMode.If)
                {
                    RunTransition();
                }
            }
        }

        [Parameter]
        public TransitionMode Mode { get; set; }

        public ElementReference Ref { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _firstRender = false;

                if (Value)
                {
                    _visible = true;
                    StateHasChanged();
                }
            }

            return Task.CompletedTask;
        }

        private void RunTransition()
        {
            Task.Run(async () =>
            {
                if (_value)
                {
                    _state = TransitionState.Enter;
                    await OnEnterAsync();
                    await InvokeAsync(StateHasChanged);

                    await Task.Delay(10);
                    _visible = true;
                    await InvokeAsync(StateHasChanged);

                    await Task.Delay(30);
                    _state = TransitionState.EnterTo;
                    await OnEnterToAsync();
                    await InvokeAsync(StateHasChanged);

                    await Task.Delay(300);
                    _state = TransitionState.None;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    _state = TransitionState.Leave;
                    await OnLeaveAsync();
                    await InvokeAsync(StateHasChanged);

                    await Task.Delay(30);
                    _state = TransitionState.LeaveTo;
                    await OnLeaveToAsync();
                    await InvokeAsync(StateHasChanged);

                    await Task.Delay(300);
                    _state = TransitionState.None;
                    _visible = false;
                    await InvokeAsync(StateHasChanged);
                }
            });
        }

        protected virtual Task OnEnterAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnEnterToAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnLeaveAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnLeaveToAsync()
        {
            return Task.CompletedTask;
        }
    }
}
