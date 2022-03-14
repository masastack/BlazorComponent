
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class KeyTransitionElement<TValue> : TransitionElementBase<TValue>
    {
        private KeyTransitionElementState<TValue>[] _states;

        protected IEnumerable<KeyTransitionElementState<TValue>> ComputedStates =>
            States.Where(state => !state.IsEmpty);

        protected KeyTransitionElementState<TValue>[] States
        {
            get
            {
                if (_states == null)
                {
                    _states = new KeyTransitionElementState<TValue>[]
                    {
                        new KeyTransitionElementState<TValue>(this),
                        new KeyTransitionElementState<TValue>(this)
                    };
                }

                return _states;
            }
        }

        protected override void StartTransition()
        {
            if (!States[1].IsEmpty)
            {
                //Last transition not complete yet
                States[1].CopyTo(States[0]);
            }
            States[1].Key = Value;

            //First render,don't trigger transition
            if (ComputedStates.Count() < 2)
            {
                return;
            }

            States[0].TransitionState = TransitionState.Leave;
            States[1].TransitionState = TransitionState.Enter;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            switch (States[1].TransitionState)
            {
                case TransitionState.Enter:
                    await OnEnterAsync();
                    break;
                case TransitionState.EnterTo:
                    await OnEnterToAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task OnEnterAsync()
        {
            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.LeaveTo, TransitionState.EnterTo);
                return Task.CompletedTask;
            });
        }

        private void NextState(TransitionState oldTransitionState, TransitionState newTransitionState)
        {
            States[0].TransitionState = oldTransitionState;
            States[1].TransitionState = newTransitionState;

            StateHasChanged();
        }

        private async Task OnEnterToAsync()
        {
            await Delay(Transition.Duration);

            //Remove old element and set new element to first position
            States[1].CopyTo(States[0]);
            States[1].Reset();

            NextState(TransitionState.None, TransitionState.None);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            foreach (var state in ComputedStates)
            {
                builder.OpenComponent<Element>(sequence++);

                builder.AddMultipleAttributes(sequence++, AdditionalAttributes);
                builder.AddAttribute(sequence++, nameof(Tag), Tag);
                builder.AddAttribute(sequence++, "class", state.ComputedClass);
                builder.AddAttribute(sequence++, "style", state.ComputedStyle);
                builder.AddAttribute(sequence++, nameof(ChildContent), RenderChildContent(state.Key));
                builder.AddComponentReferenceCapture(sequence++, el => Reference = ((Element)el).Reference);
                builder.SetKey(state.Key);

                builder.CloseComponent();
            }
        }

        private RenderFragment RenderChildContent(TValue key)
        {
            return builder =>
            {
                var sequence = 0;
                builder.OpenComponent<Container>(sequence++);
                builder.AddAttribute(sequence++, nameof(Container.Value), EqualityComparer<TValue>.Default.Equals(key, Value));
                builder.AddAttribute(sequence++, nameof(ChildContent), ChildContent);
                builder.CloseComponent();
            };
        }
    }
}
