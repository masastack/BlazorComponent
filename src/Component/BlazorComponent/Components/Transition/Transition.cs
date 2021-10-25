using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace BlazorComponent
{
    public partial class Transition : ComponentBase
    {
        private CancellationTokenSource _cancellationTokenSource;

        [Parameter]
        public string Name { get; set; } = "m";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Func<Task> OnEnterTo { get; set; }

        [Parameter]
        public Func<Task> OnBeforeEnter { get; set; }

        [Parameter]
        public Func<Task> OnAfterEnter { get; set; }

        [Parameter]
        public Func<Task> OnBeforeLeave { get; set; }

        [Parameter]
        public Func<Task> OnAfterLeave { get; set; }

        protected bool Show { get; set; }

        public bool If { get; protected set; } = true;

        protected TransitionState State { get; private set; }

        protected CssBuilder CssBuilder { get; set; } = new();

        protected StyleBuilder StyleBuilder { get; set; } = new();

        protected Element FirstElement { get; set; }

        public string Class => CssBuilder.Class;

        public string Style => StyleBuilder.Style;

        protected override void OnInitialized()
        {
            CssBuilder
                .AddIf(() => $"{Name}-enter {Name}-enter-active", () => State == TransitionState.Enter)
                .AddIf(() => $"{Name}-enter-active {Name}-enter-to", () => State == TransitionState.EnterTo)
                .AddIf(() => $"{Name}-leave {Name}-leave-active", () => State == TransitionState.Leave)
                .AddIf(() => $"{Name}-leave-active {Name}-leave-to", () => State == TransitionState.LeaveTo);

            StyleBuilder
                .AddIf("display:none!important", () => !Show);
        }

        public bool Register(Element element)
        {
            if (FirstElement == null)
            {
                FirstElement = element;
                return true;
            }

            return false;
        }

        public void RunTransition(TransitionMode mode, bool value)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            _ = Task.Run(async () =>
              {
                  if (value)
                  {
                      if (mode == TransitionMode.If && !If)
                      {
                          If = true;
                          await FirstElement.UpdateViewAsync();
                      }
                      await OnBeforeEnterAsync();

                      State = TransitionState.Enter;
                      await OnEnterAsync();
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(16, _cancellationTokenSource.Token);
                      Show = true;
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(16, _cancellationTokenSource.Token);
                      State = TransitionState.EnterTo;
                      await OnEnterToAsync();
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(300, _cancellationTokenSource.Token);
                      State = TransitionState.None;
                      await FirstElement.UpdateViewAsync();

                      await OnAfterEnterAsync();
                  }
                  else
                  {
                      await OnBeforeLeaveAsync();

                      State = TransitionState.Leave;
                      await OnLeaveAsync();
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(16, _cancellationTokenSource.Token);
                      State = TransitionState.LeaveTo;
                      await OnLeaveToAsync();
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(300, _cancellationTokenSource.Token);
                      Show = false;
                      State = TransitionState.None;
                      await FirstElement.UpdateViewAsync();

                      await OnAfterLeaveAsync();
                      if (mode == TransitionMode.If && If)
                      {
                          If = false;
                          await FirstElement.UpdateViewAsync();
                      }
                  }
              });
        }

        protected virtual async Task OnBeforeEnterAsync()
        {
            if (OnBeforeEnter != null)
            {
                await OnBeforeEnter?.Invoke();
            }
        }

        protected virtual Task OnEnterAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual async Task OnAfterEnterAsync()
        {
            if (OnAfterEnter != null)
            {
                await OnAfterEnter?.Invoke();
            }
        }

        protected virtual async Task OnEnterToAsync()
        {
            if (OnEnterTo != null)
            {
                await OnEnterTo?.Invoke();
            }
        }

        protected virtual async Task OnBeforeLeaveAsync()
        {
            if (OnBeforeLeave != null)
            {
                await OnBeforeLeave?.Invoke();
            }
        }

        protected virtual Task OnLeaveAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual async Task OnAfterLeaveAsync()
        {
            if (OnAfterLeave != null)
            {
                await OnAfterLeave?.Invoke();
            }
        }

        protected virtual Task OnLeaveToAsync()
        {
            return Task.CompletedTask;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (FirstElement.Show)
                {
                    Show = true;
                    await FirstElement.UpdateViewAsync();
                }
                else if (FirstElement.If)
                {
                    RunTransition(TransitionMode.If, true);
                }
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            builder.OpenComponent<CascadingValue<Transition>>(sequence++);

            builder.AddAttribute(sequence++, "Value", this);
            builder.AddAttribute(sequence++, "IsFixed", true);
            builder.AddAttribute(sequence++, "ChildContent", ChildContent);

            builder.CloseComponent();
        }
    }
}
