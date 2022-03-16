using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract class ToggleableTransitionElement : TransitionElementBase<bool>
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public override IDictionary<string, object> AdditionalAttributes
        {
            get
            {
                var attributes = base.AdditionalAttributes ?? new Dictionary<string, object>();

                attributes["class"] = ComputedClass;
                attributes["style"] = ComputedStyle;

                return attributes;
            }
            set
            {
                base.AdditionalAttributes = value;
            }
        }

        protected override string ComputedClass
        {
            get
            {
                var transitionClass = Transition.GetClass(TransitionState);
                return string.Join(" ", Class, transitionClass);
            }
        }

        protected override string ComputedStyle
        {
            get
            {
                var transitionStyle = Transition.GetStyle(TransitionState);
                return string.Join(';', Style, transitionStyle);
            }
        }

        protected TransitionState TransitionState { get; private set; }

        protected bool TransitionStateChanged { get; private set; }

        protected bool LazyValue { get; set; }

        protected override void StartTransition()
        {
            //Don't trigger transition in first render
            if (FirstRender)
            {
                LazyValue = Value;
                return;
            }

            //No transition
            if (Transition == null || string.IsNullOrEmpty(Transition.Name))
            {
                LazyValue = Value;
                return;
            }

            if (Value)
            {
                ShowElement();
                NextState(TransitionState.Enter);
            }
            else
            {
                NextState(TransitionState.Leave);
            }
        }

        protected void ShowElement()
        {
            LazyValue = true;
        }

        protected override bool ShouldRender()
        {
            return TransitionState == TransitionState.None || TransitionStateChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Transition.OnElementReadyAsync(this);
            }

            switch (TransitionState)
            {
                case TransitionState.Enter:
                    await OnEnterAsync();
                    break;
                case TransitionState.EnterTo:
                    await OnEnterToAsync();
                    break;
                case TransitionState.Leave:
                    await OnLeaveAsync();
                    break;
                case TransitionState.LeaveTo:
                    await OnLeaveToAsync();
                    break;
                default:
                    break;
            }
        }

        protected virtual async Task OnEnterAsync()
        {
            if (Transition.OnEnter.HasDelegate)
            {
                await Transition.OnEnter.InvokeAsync(this);
            }

            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.EnterTo);
                return Task.CompletedTask;
            });
        }

        private void NextState(TransitionState transitionState)
        {
            TransitionState = transitionState;

            TransitionStateChanged = true;
            StateHasChanged();
            TransitionStateChanged = false;
        }

        protected virtual async Task OnEnterToAsync()
        {
            if (Transition.OnEnterTo.HasDelegate)
            {
                await Transition.OnEnterTo.InvokeAsync(this);
            }

            await Delay(Transition.Duration);
            NextState(TransitionState.None);
        }

        protected virtual async Task OnLeaveAsync()
        {
            if (Transition.OnLeave.HasDelegate)
            {
                await Transition.OnLeave.InvokeAsync(this);
            }

            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.LeaveTo);
                return Task.CompletedTask;
            });
        }

        protected virtual async Task OnLeaveToAsync()
        {
            if (Transition.OnLeaveTo.HasDelegate)
            {
                await Transition.OnLeaveTo.InvokeAsync(this);
            }

            await Delay(Transition.Duration);

            HideElement();
            NextState(TransitionState.None);
        }

        protected void HideElement()
        {
            LazyValue = false;
        }
    }
}
