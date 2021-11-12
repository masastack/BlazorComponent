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

        [CascadingParameter]
        public Transition CascadingTransition { get; set; }

        [Parameter]
        public string Origin { get; set; }

        protected bool? Show { get; set; } = false;

        public bool? If { get; protected set; } = true;

        protected TransitionState State { get; private set; }

        protected CssBuilder CssBuilder { get; set; } = new();

        protected StyleBuilder StyleBuilder { get; set; } = new();

        protected Element FirstElement { get; set; }

        public string Class => CssBuilder.Class;

        public string Style => StyleBuilder.Style;

        protected string ComputedName
        {
            get
            {
                if (CascadingTransition != null && CascadingTransition.ComputedState != TransitionState.None)
                {
                    return CascadingTransition.ComputedName;
                }

                return Name;
            }
        }

        protected TransitionState ComputedState
        {
            get
            {
                if (CascadingTransition != null && CascadingTransition.ComputedState != TransitionState.None)
                {
                    return CascadingTransition.ComputedState;
                }

                return State;
            }
        }

        protected TransitionMode Mode { get; private set; }

        protected override void OnInitialized()
        {
            CssBuilder
                .AddIf(() => $"{ComputedName}-enter {ComputedName}-enter-active", () => State == TransitionState.Enter)
                .AddIf(() => $"{ComputedName}-enter-active {ComputedName}-enter-to", () => State == TransitionState.EnterTo)
                .AddIf(() => $"{ComputedName}-leave {ComputedName}-leave-active", () => State == TransitionState.Leave)
                .AddIf(() => $"{ComputedName}-leave-active {ComputedName}-leave-to", () => State == TransitionState.LeaveTo);

            StyleBuilder
                .AddIf("display:none", () => Show == false)
                .AddIf(() => $"transform-origin:{Origin}", () => !string.IsNullOrEmpty(Origin) && State != TransitionState.None);
        }

        public bool Register(Element element)
        {
            if (FirstElement == null)
            {
                FirstElement = element;
                if (FirstElement.Show != Show)
                {
                    Show = FirstElement.Show;
                }
                if (FirstElement.If != If)
                {
                    If = FirstElement.If;
                }

                return true;
            }

            return false;
        }

        public void RunTransition(TransitionMode mode, bool value = false)
        {
            Mode = mode;
            _ = Task.Run(async () =>
              {
                  _cancellationTokenSource?.Cancel();
                  if (_cancellationTokenSource != null && _cancellationTokenSource.Token.IsCancellationRequested)
                  {
                      await OnCancelledAsync();
                  }
                  _cancellationTokenSource = new CancellationTokenSource();

                  if (value)
                  {
                      if (mode == TransitionMode.If)
                      {
                          //TODO: We need fix here
                          If = true;
                      }

                      await OnBeforeEnterAsync();

                      if (mode == TransitionMode.Show)
                      {
                          Show = true;
                      }

                      State = TransitionState.Enter;
                      await OnEnterAsync();
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(16, _cancellationTokenSource.Token);
                      State = TransitionState.EnterTo;
                      await OnEnterToAsync();
                      await FirstElement.UpdateViewAsync();

                      await Task.Delay(300, _cancellationTokenSource.Token);
                      State = TransitionState.None;
                      await OnAfterEnterAsync();
                      await FirstElement.UpdateViewAsync();
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
                      if (mode == TransitionMode.Show)
                      {
                          Show = false;
                      }
                      State = TransitionState.None;
                      await OnAfterLeaveAsync();

                      if (mode == TransitionMode.If)
                      {
                          If = false;
                      }
                      await FirstElement.UpdateViewAsync();
                  }
              });
        }

        protected virtual async Task OnCancelledAsync()
        {
            if (State != TransitionState.None && Mode == TransitionMode.Show)
            {
                if (State == TransitionState.Leave || State == TransitionState.LeaveTo)
                {
                    Show = false;
                }

                State = TransitionState.None;
                await FirstElement.UpdateViewAsync();
            }
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
