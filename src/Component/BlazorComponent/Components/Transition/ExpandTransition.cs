using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public class ExpandTransition : Transition
    {
        [Inject]
        public IJSRuntime Js { get; set; }

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
            return string.Join(" ", transitionClass);
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
            // TODO: record this element. go to #72
            await Js.InvokeVoidAsync(JsInteropConstants.ObserveElement, element.Reference, SizeProp, DotNetObjectReference.Create(this));
        }

        // internal override async Task EnterAsync(ElementReference el)
        // {
        //     var element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, el);
        //     Console.WriteLine($"element:{element.OffsetHeight}");
        //     Size = element.OffsetHeight;
        // }

        [JSInvokable]
        public void OnSizeChanged(double size)
        {
            // TODO: 触发的ID不一样？试试把referenceid传进来
            Console.WriteLine($"Size:{Size}");
            Size = size;
        }
    }
}