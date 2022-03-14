using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ExpandTransition : Transition
    {
        [Inject]
        public IJSRuntime JS { get; set; }

        protected virtual string SizeProp => "height";

        protected double Size { get; set; }

        protected override void OnParametersSet()
        {
            Name = "expand-transition";
        }

        public override string GetClass(TransitionState transitionState)
        {
            var transitionClass = base.GetClass(transitionState);
            return string.Join(" ", transitionClass, transitionState == TransitionState.None ? null : "in-transition");
        }

        public override string GetStyle(TransitionState transitionState)
        {
            var styles = new List<string>
            {
                base.GetStyle(transitionState)
            };

            switch (transitionState)
            {
                case TransitionState.Enter:
                case TransitionState.LeaveTo:
                    styles.Add($"{SizeProp}:0px");
                    break;
                case TransitionState.EnterTo:
                case TransitionState.Leave:
                    styles.Add($"{SizeProp}:{Size}px");
                    break;
                default:
                    break;
            }

            if (transitionState != TransitionState.None)
            {
                styles.Add("overflow:hidden");
            }

            return string.Join(';', styles);
        }

        public override async Task OnElementReadyAsync(ToggleableTransitionElement element)
        {
            await JS.InvokeVoidAsync(JsInteropConstants.ObserveElement, element.Reference, SizeProp, DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void OnSizeChanged(double size)
        {
            Size = size;
        }
    }
}
