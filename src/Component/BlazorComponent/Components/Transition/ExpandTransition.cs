using BlazorComponent.Web;
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

        protected virtual bool X { get; }

        protected string SizeProp => X ? "width" : "height";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            StyleBuilder
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
            _size = await Element.GetSizeAsync(prop);

            await base.OnEnterAsync();
        }

        protected override async Task OnLeaveAsync()
        {
            var prop = SizeProp.Substring(0, 1).ToUpper() + SizeProp[1..];
            _size = await Element.GetSizeAsync(prop);

            await base.OnEnterAsync();
        }
    }
}
