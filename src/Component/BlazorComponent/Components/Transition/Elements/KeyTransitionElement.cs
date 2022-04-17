using System.Reflection.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Util.Reflection.Expressions.IntelligentGeneration.Extensions;

namespace BlazorComponent
{
    public class KeyTransitionElement<TValue> : TransitionElementBase<TValue>
    {
        private KeyTransitionElementState<TValue>[] _states;

        protected IEnumerable<KeyTransitionElementState<TValue>> ComputedStates =>
            States.Where(state => !state.IsEmpty);

        protected KeyTransitionElementState<TValue>[] States =>
            _states ??= new KeyTransitionElementState<TValue>[]
            {
                new(this),
                new(this)
            };

        protected override TransitionState CurrentState
        {
            get
            {
                if (Transition is not null && Transition.Mode == TransitionMode.InOut)
                {
                    if (States[1].TransitionState != TransitionState.None)
                    {
                        return States[1].TransitionState;
                    }

                    return States[0].TransitionState;
                }
                else
                {
                    if (States[0].TransitionState != TransitionState.None)
                    {
                        return States[0].TransitionState;
                    }

                    return States[1].TransitionState;
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (_referenceChanged)
            {
                _referenceChanged = false;

                await InteropCall();
            }

            await NextAsync(CurrentState);
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

        protected override async Task BeforeLeave()
        {
            if (!FirstRender && Transition.LeaveAbsolute)
            {
                Element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Reference);
            }
        }

        private async Task NextAsync(TransitionState state)
        {
            if (Transition is null)
            {
                return;
            }

            if (!Transition.Mode.HasValue)
            {
                switch (state)
                {
                    case TransitionState.Leave:
                    case TransitionState.Enter:
                        await RequestNextState(TransitionState.LeaveTo, TransitionState.EnterTo);
                        break;
                }
            }
            else if (Transition.Mode == TransitionMode.OutIn)
            {
                switch (state)
                {
                    case TransitionState.Leave:
                        await RequestNextState(TransitionState.LeaveTo, TransitionState.None);
                        break;
                    case TransitionState.Enter:
                        await RequestNextState(TransitionState.None, TransitionState.EnterTo);
                        break;
                }
            }
            else if (Transition.Mode == TransitionMode.InOut)
            {
                switch (state)
                {
                    case TransitionState.Enter:
                        await RequestNextState(TransitionState.None, TransitionState.EnterTo);
                        break;
                    case TransitionState.Leave:
                        await RequestNextState(TransitionState.LeaveTo, TransitionState.None);
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

            if (Transition.Mode == TransitionMode.OutIn)
            {
                if (CurrentState == TransitionState.LeaveTo)
                {
                    NextState(TransitionState.None, TransitionState.Enter);
                }
            }
            else if (Transition.Mode == TransitionMode.InOut)
            {
                // TODO: InOut mode
            }
            else if (!Transition.Mode.HasValue)
            {
                if (CurrentState == TransitionState.LeaveTo)
                {
                    //Remove old element and set new element to first position
                    States[1].CopyTo(States[0]);
                    States[1].Reset();

                    NextState(TransitionState.None, TransitionState.None);
                }
            }

            Console.WriteLine("invoke on transition end");
        }

        private void NextState(TransitionState oldTransitionState, TransitionState newTransitionState)
        {
            States[0].TransitionState = oldTransitionState;
            States[1].TransitionState = newTransitionState;

            StateHasChanged();
        }

        private async Task RequestNextState(TransitionState first, TransitionState second)
        {
            await RequestAnimationFrameAsync(() =>
            {
                NextState(first, second);
                return Task.CompletedTask;
            });
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;

            List<KeyTransitionElementState<TValue>> filteredStates = new();

            if (Transition.Mode == TransitionMode.OutIn)
            {
                var state = ComputedStates.FirstOrDefault(s => s.TransitionState != TransitionState.None);

                state ??= ComputedStates.LastOrDefault();

                if (state is not null)
                {
                    filteredStates.Add(state);
                }
            }
            else
            {
                filteredStates = ComputedStates
                    .ToList();
            }

            foreach (var state in filteredStates)
            {
                builder.OpenElement(sequence++, Tag);

                builder.AddMultipleAttributes(sequence++, AdditionalAttributes);
                builder.AddAttribute(sequence++, "class", state.ComputedClass);
                builder.AddAttribute(sequence++, "style", state.ComputedStyle);
                builder.AddContent(sequence++, RenderChildContent(state.Key));
                builder.AddElementReferenceCapture(sequence++, reference =>
                {
                    ReferenceCaptureAction?.Invoke(reference);
                    Reference = reference;
                });

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