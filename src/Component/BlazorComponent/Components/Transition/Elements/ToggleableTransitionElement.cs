﻿namespace BlazorComponent
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
            set => base.AdditionalAttributes = value;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && Transition is not null)
            {
                await Transition.OnElementReadyAsync(this);
            }
        }

        private TransitionState State { get;  set; }

        protected bool LazyValue { get; private set; }

        protected override string ComputedClass
        {
            get
            {
                if (Transition == null) return Class;

                var transitionClass = Transition.GetClass(State);
                return string.Join(" ", Class, transitionClass);
            }
        }

        protected override string ComputedStyle
        {
            get
            {
                if (Transition == null) return Style;

                var transitionStyle = Transition.GetStyle(State);
                return string.Join(';', Style, transitionStyle);
            }
        }

        protected override TransitionState CurrentState => State;

        protected override void StartTransition()
        {
            Console.WriteLine($"StartTransition: {Reference.Id}; FirstRender:{FirstRender}");

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
                State = TransitionState.Enter;
            }
            else
            {
                State = TransitionState.Leave;
            }
        }

        protected override async Task NextAsync(TransitionState state)
        {
            Console.WriteLine($"{Reference.Id}: {state} to next state");

            switch (state)
            {
                case TransitionState.Enter:
                    await RequestNextStateAsync(TransitionState.EnterTo);
                    break;
                case TransitionState.Leave:
                    await RequestNextStateAsync(TransitionState.LeaveTo);
                    break;
            }
        }

        protected override Task OnTransitionEndAsync(string referenceId, LeaveEnter transition)
        {
            Console.WriteLine($"{Reference.Id} OnTransitionEnd {referenceId} {transition}");
            
            if (referenceId != Reference.Id)
            {
                return Task.CompletedTask;
            }

            if (transition == LeaveEnter.Enter && CurrentState == TransitionState.EnterTo)
            {
                NextState(TransitionState.None);
            }
            else if (transition == LeaveEnter.Leave && CurrentState == TransitionState.LeaveTo)
            {
                HideElement();
                NextState(TransitionState.None);
            }

            return Task.CompletedTask;
        }

        private void NextState(TransitionState transitionState)
        {
            State = transitionState;
            StateHasChanged();
        }

        private async Task RequestNextStateAsync(TransitionState state)
        {
            await RequestAnimationFrameAsync(() =>
            {
                NextState(state);
                return Task.CompletedTask;
            });
        }

        private void HideElement()
        {
            LazyValue = false;
        }

        private void ShowElement()
        {
            LazyValue = true;
        }
    }
}