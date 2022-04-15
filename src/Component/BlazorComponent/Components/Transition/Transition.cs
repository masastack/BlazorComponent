using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class Transition : ComponentBase
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Origin { get; set; }

        [Parameter]
        public int Duration { get; set; } // TODO: 先实现css的动画时间

        [Parameter]
        public bool LeaveAbsolute { get; set; }

        [Parameter]
        public TransitionMode? Mode { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<Element> OnEnter { get; set; }

        [Parameter]
        public EventCallback<Element> OnLeave { get; set; }

        [Parameter]
        public EventCallback<Element> OnEnterTo { get; set; }

        [Parameter]
        public EventCallback<Element> OnLeaveTo { get; set; }

        private StyleBuilder StyleBuilder { get; set; } = new();
        internal BlazorComponent.Web.Element? Element = null;

        public virtual string GetClass(TransitionState transitionState)
        {
            return transitionState switch
            {
                TransitionState.Enter => $"{Name}-enter {Name}-enter-active",
                TransitionState.EnterTo => $"{Name}-enter-active {Name}-enter-to",
                TransitionState.Leave => $"{Name}-leave {Name}-leave-active",
                TransitionState.LeaveTo => $"{Name}-leave-active {Name}-leave-to",
                _ => null
            };
        }

        public virtual string GetStyle(TransitionState transitionState)
        {
            if (Origin != null && transitionState != TransitionState.None)
            {
                return $"transform-origin:{Origin}";
            }

            return null;
        }

        public void Leave()
        {
            StateHasChanged();
        }

        public virtual Task OnElementReadyAsync(ToggleableTransitionElement element)
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