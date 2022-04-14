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
            
            NextState(0, TransitionState.Leave);
            
            States[0].TransitionState = TransitionState.Leave;
            States[1].TransitionState = TransitionState.Enter;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (Transition.Name.StartsWith("picker-"))
            {
                Console.WriteLine($"{_referenceChanged} {Reference.Id}");
            }

            if (_referenceChanged)
            {
                _referenceChanged = false;

                await InteropCall();
            }

            if (Transition.Mode == TransitionMode.OutIn)
            {
                if (States[0].TransitionState == TransitionState.Leave)
                {
                    await OnLeaveAsync();
                }
                else if (States[0].TransitionState == TransitionState.None && States[1].TransitionState == TransitionState.Enter)
                {
                    await OnEnterAsync();
                }
            }
            else
            {
                if (States[0].TransitionState != TransitionState.None)
                {
                    if (States[0].TransitionState == TransitionState.Leave)
                    {
                        await OnLeaveAsync(0);
                    }

                    if (States[1].TransitionState == TransitionState.Enter)
                    {
                        await OnEnterAsync(1);
                    }
                }
            }
        }

        private async Task OnLeaveAndEnterAsync()
        {
            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.LeaveTo, TransitionState.EnterTo);
                return Task.CompletedTask;
            });
        }

        private async Task OnLeaveToAndEnterToAsync()
        {
            // await Delay(Transition.Duration);

            //Remove old element and set new element to first position
            States[1].CopyTo(States[0]);
            States[1].Reset();

            NextState(TransitionState.None, TransitionState.None);
        }

        private async Task OnLeaveAsync(int? index = null)
        {
            await RequestAnimationFrameAsync(() =>
            {
                if (index.HasValue)
                {
                    NextState(index.Value, TransitionState.LeaveTo);
                }
                else
                {
                    NextState(TransitionState.LeaveTo, TransitionState.Enter);
                }

                return Task.CompletedTask;
            });
        }

        private async Task OnEnterAsync(int? index = null)
        {
            // await Delay(300);

            await RequestAnimationFrameAsync(() =>
            {
                if (index.HasValue)
                {
                    NextState(index.Value, TransitionState.EnterTo);
                }
                else
                {
                    NextState(TransitionState.None, TransitionState.EnterTo);
                }

                return Task.CompletedTask;
            });
        }

        private async Task OnEnterToAsync()
        {
            // await Delay(Transition.Duration);

            //Remove old element and set new element to first position
            // States[1].CopyTo(States[0]);
            // States[1].Reset();

            States[0].Reset();

            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.None, TransitionState.None);
                return Task.CompletedTask;
            });
        }

        protected override async Task OnTransitionEnd(string referenceId, LeaveEnter transition)
        {
            if (referenceId != Reference.Id)
            {
                return;
            }

            Console.WriteLine("invoke on transition end");

            if (Transition.Mode == TransitionMode.OutIn)
            {
                if (States[0].TransitionState == TransitionState.LeaveTo)
                {
                    NextState(TransitionState.None, TransitionState.Enter);
                }
            }
            else if (Transition.Mode == TransitionMode.InOut)
            {
            }
            else if (!Transition.Mode.HasValue)
            {
                if (States[0].TransitionState == TransitionState.LeaveTo)
                {
                    // NextState(TransitionState.None, States[1].TransitionState);
                    NextState(0, TransitionState.None);
                    await OnEnterToAsync();
                }
            }
        }

        protected override Task OnTransitionCancel()
        {
            Console.WriteLine("transition cancel.....");

            return base.OnTransitionCancel();
        }

        private void NextState(TransitionState oldTransitionState, TransitionState newTransitionState)
        {
            States[0].TransitionState = oldTransitionState;
            States[1].TransitionState = newTransitionState;

            StateHasChanged();
        }

        private async Task NextState(int index, TransitionState state)
        {
            if (index == 0 && state == TransitionState.Leave)
            {
                // TODO: await?

                // if (Transition!.LeaveAbsolute)
                // {
                //     Transition!.Element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Reference);
                //     Transition!.Leave();
                // }
            }
            
            States[index].TransitionState = state;
            StateHasChanged();
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
                filteredStates = ComputedStates.ToList();
            }

            foreach (var state in filteredStates)
            {
                Console.WriteLine(state.Key);

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
                // builder.SetKey(state.Key);

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