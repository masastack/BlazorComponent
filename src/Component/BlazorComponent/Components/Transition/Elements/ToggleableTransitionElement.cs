using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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
            set { base.AdditionalAttributes = value; }
        }

        protected override string ComputedClass
        {
            get
            {
                if (Transition != null)
                {
                    var transitionClass = Transition.GetClass(TransitionState);
                    return string.Join(" ", Class, transitionClass);
                }
                else
                {
                    return Class;
                }
            }
        }

        protected override string ComputedStyle
        {
            get
            {
                if (Transition != null)
                {
                    var transitionStyle = Transition.GetStyle(TransitionState);
                    return string.Join(';', Style, transitionStyle);
                }
                else
                {
                    return Style;
                }
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (Transition != null)
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
                    case TransitionState.Leave:
                        await OnLeaveAsync();
                        break;
                }
            }
        }

        protected override async Task OnTransitionEnd(string referenceId, LeaveEnter transition)
        {
            if (referenceId != Reference.Id)
            {
                return;
            }

            Console.WriteLine($"2: referenceId:{referenceId}, Reference.Id:{Reference.Id}, transition:{transition.ToString()}");

            if (transition == LeaveEnter.Leave)
            {
                await OnLeaveToAsync();
            }
            else if (transition == LeaveEnter.Enter)
            {
                await OnEnterToAsync();
            }

            Console.WriteLine($"{DateTime.Now.Second},toggle transition ends.");
        }

        protected override Task OnTransitionCancel()
        {
            Console.WriteLine($"{DateTime.Now.Second},toggle transition cancel.");
            return base.OnTransitionCancel();
        }

        protected virtual async Task OnEnterAsync()
        {
            if (Transition is not null && Transition.OnEnter.HasDelegate)
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
            StateHasChanged();
        }

        protected virtual async Task OnEnterToAsync()
        {
            if (Transition is not null)
            {
                if (Transition.OnEnterTo.HasDelegate)
                {
                    await Transition.OnEnterTo.InvokeAsync(this);
                }

                await Task.Delay(Transition.Duration);
            }

            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.None);
                return Task.CompletedTask;
            });
        }

        protected virtual async Task OnLeaveAsync()
        {
            if (Transition is not null)
            {
                if (Transition.LeaveAbsolute)
                {
                    // TODO: LeaveAbsolute
                }
                
                if (Transition.OnLeave.HasDelegate)
                {
                    await Transition.OnLeave.InvokeAsync(this);
                }
            }

            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.LeaveTo);
                return Task.CompletedTask;
            });
        }

        protected virtual async Task OnLeaveToAsync()
        {
            if (Transition is not null)
            {
                if (Transition.OnLeaveTo.HasDelegate)
                {
                    await Transition.OnLeaveTo.InvokeAsync(this);
                }

                await Task.Delay(Transition.Duration);
            }

            HideElement();
            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.None);
                return Task.CompletedTask;
            });
        }

        protected void HideElement()
        {
            LazyValue = false;
        }
    }
}