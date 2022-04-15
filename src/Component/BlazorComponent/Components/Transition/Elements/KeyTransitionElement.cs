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
            States.Where(state => !state.IsEmpty && !state.IsHook);

        protected KeyTransitionElementState<TValue>[] States =>
            _states ??= new KeyTransitionElementState<TValue>[]
            {
                new(this),
                new(this)
            };

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

            States[0].TransitionState = TransitionState.BeforeLeave;
            States[1].TransitionState = TransitionState.BeforeEnter;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (_referenceChanged)
            {
                _referenceChanged = false;

                await InteropCall();
            }

            if (Transition.Mode == TransitionMode.OutIn)
            {
                if (States[0].TransitionState == TransitionState.BeforeLeave)
                {
                    // TODO: check
                    await BeforeLeaveAsync();
                }
                else if (States[0].TransitionState == TransitionState.Leave)
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
                    if (States[0].TransitionState == TransitionState.BeforeLeave)
                    {
                        await BeforeLeaveEnterAsync();
                    }
                    else if (States[0].TransitionState == TransitionState.Leave)
                    {
                        await OnLeaveAndEnterAsync();
                    }
                }
            }
        }

        private async Task BeforeLeaveEnterAsync()
        {
            if (Transition is not null)
            {
                if (Transition.LeaveAbsolute)
                {
                    Element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Reference);
                }
            }

            await RequestAnimationFrameAsync(() =>
            {
                NextState(TransitionState.Leave, TransitionState.Enter);
                return Task.CompletedTask;
            });
        }

        private void NextAsync(TransitionState state)
        {
            if (Transition is null)
            {
                return;
            }

            if (!Transition.Mode.HasValue)
            {
                switch (state)
                {
                    case TransitionState.BeforeLeave:
                    case TransitionState.BeforeEnter:
                        NextState(TransitionState.Leave, TransitionState.Enter);
                        break;
                    case TransitionState.Leave:
                    case TransitionState.Enter:
                        NextState(TransitionState.LeaveTo, TransitionState.EnterTo);
                        break;
                    case TransitionState.LeaveTo:
                    case TransitionState.EnterTo:
                        NextState(TransitionState.None, TransitionState.None);
                        break;
                }
            }
            else if (Transition.Mode == TransitionMode.OutIn)
            {
                switch (state)
                {
                    case TransitionState.BeforeEnter:
                        NextState(TransitionState.None, TransitionState.Enter);
                        break;
                    case TransitionState.Enter:
                        NextState(TransitionState.None, TransitionState.EnterTo);
                        break;
                    case TransitionState.EnterTo:
                        NextState(TransitionState.None, TransitionState.None);
                        break;
                    case TransitionState.BeforeLeave:
                        NextState(TransitionState.Leave, TransitionState.None);
                        break;
                    case TransitionState.Leave:
                        NextState(TransitionState.LeaveTo, TransitionState.None);
                        break;
                    case TransitionState.LeaveTo:
                        NextState(TransitionState.None, TransitionState.BeforeEnter);
                        break;
                }
            }
            else if (Transition.Mode == TransitionMode.InOut)
            {
                switch (state)
                {
                    case TransitionState.BeforeEnter:
                        NextState(TransitionState.None, TransitionState.Enter);
                        break;
                    case TransitionState.Enter:
                        NextState(TransitionState.None, TransitionState.EnterTo);
                        break;
                    case TransitionState.EnterTo:
                        NextState(TransitionState.BeforeLeave, TransitionState.None);
                        break;
                    case TransitionState.BeforeLeave:
                        NextState(TransitionState.Leave, TransitionState.None);
                        break;
                    case TransitionState.Leave:
                        NextState(TransitionState.LeaveTo, TransitionState.None);
                        break;
                    case TransitionState.LeaveTo:
                        NextState(TransitionState.None, TransitionState.None);
                        break;
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
            //Remove old element and set new element to first position
            States[1].CopyTo(States[0]);
            States[1].Reset();

            NextState(TransitionState.None, TransitionState.None);
        }

        private async Task BeforeLeaveAsync(int? index = null)
        {
            if (Transition is not null)
            {
                if (Transition.LeaveAbsolute)
                {
                    Element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Reference);
                }
            }

            await RequestAnimationFrameAsync(() =>
            {
                if (index.HasValue)
                {
                    NextState(index.Value, TransitionState.Leave);
                }
                else
                {
                    NextState(TransitionState.Leave, TransitionState.None);
                }

                return Task.CompletedTask;
            });
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
                    NextState(TransitionState.LeaveTo, TransitionState.BeforeEnter);
                }

                return Task.CompletedTask;
            });
        }

        private async Task BeforeEnterAsync(int? index = null)
        {
            if (Transition is not null)
            {
            }

            await RequestAnimationFrameAsync(() =>
            {
                if (index.HasValue)
                {
                    NextState(index.Value, TransitionState.Enter);
                }
                else
                {
                    NextState(TransitionState.None, TransitionState.Enter);
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
                    // NextState(0, TransitionState.None);
                    // await OnEnterToAsync();
                    await OnLeaveToAndEnterToAsync();
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

        private void NextState(int index, TransitionState state)
        {
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