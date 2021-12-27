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
        [Inject]
        public Document Document { get; set; }

        protected virtual bool X { get; }

        protected double? Size { get; set; }

        protected string SizeProp => X ? "width" : "height";

        protected override void OnInitialized()
        {
            base.OnInitialized();

            StyleBuilder
                .AddIf(() => $"{SizeProp}:0px", () => State == TransitionState.Enter || State == TransitionState.LeaveTo)
                .AddIf("overflow:hidden", () => State != TransitionState.None)
                .AddIf(() => $"{SizeProp}:{Size}px", () => (State == TransitionState.EnterTo || State == TransitionState.Leave) && Size != null);
        }

        protected override void OnParametersSet()
        {
            Name = X ? "expand-x-transition" : "expand-transition";
        }

        protected override async Task OnBeforeEnterAsync()
        {
            var prop = SizeProp[..1].ToUpper() + SizeProp[1..];
            var el = Document.GetElementByReference(FirstElement.Reference);
            Size = await el.GetSizeAsync(prop);

            await base.OnBeforeEnterAsync();
        }

        protected override async Task OnBeforeLeaveAsync()
        {
            var prop = SizeProp[..1].ToUpper() + SizeProp[1..];
            var el = Document.GetElementByReference(FirstElement.Reference);
            Size = await el.GetSizeAsync(prop);

            await base.OnBeforeLeaveAsync();
        }
    }
}
