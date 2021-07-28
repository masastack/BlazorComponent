using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ExpandTransition : Transition
    {
        private double _size;

        [Inject]
        public IJSRuntime Js { get; set; }

        [Parameter]
        public bool X { get; set; }

        public string SizeProp => X ? "width" : "height";

        public ExpandTransition()
            : base()
        {
            Context.StyleBuilder
                .AddIf(() => $"{SizeProp}:0px", () => State == TransitionState.Enter || State == TransitionState.LeaveTo)
                .AddIf("overflow:hidden", () => State != TransitionState.None)
                .AddIf(() => $"{SizeProp}:{_size}px", () => State == TransitionState.EnterTo || State == TransitionState.Leave);
        }

        protected override void OnParametersSet()
        {
            Name = X ? "expand-x-transition" : "expand-transition";
        }

        protected override async Task OnEnterAsync()
        {
            var prop = SizeProp.Substring(0, 1).ToUpper() + SizeProp[1..];
            _size = await Js.InvokeAsync<double>(JsInteropConstants.GetSize, Ref, prop);

            await base.OnEnterAsync();
        }

        protected override async Task OnLeaveAsync()
        {
            var prop = SizeProp.Substring(0, 1).ToUpper() + SizeProp[1..];
            _size = await Js.InvokeAsync<double>(JsInteropConstants.GetSize, Ref, prop);

            await base.OnEnterAsync();
        }
    }
}
