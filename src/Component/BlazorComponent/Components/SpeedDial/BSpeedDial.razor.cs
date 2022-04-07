using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BSpeedDial<TButton>: BBootable where TButton : BButton
    {
        [Inject]
        public Document Document { get; set; }

        [Parameter]
        public string Direction { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public string Transition { get; set; }

        public List<TButton> Buttons { get; } = new();

        public Task AddButton(TButton button)
        {
            Buttons.Add(button);

            StateHasChanged();

            return Task.CompletedTask;
        }

        protected override async Task OnActiveUpdated(bool value)
        {
            if (!OpenOnHover)
            {
                await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                    DotNetObjectReference.Create(new Invoker<object>(HandleOutsideClickAsync)),
                    new[] { Document.GetElementByReference(Ref).Selector }, null, Ref);
            }

            await base.OnActiveUpdated(value);
        }

        private async Task HandleOutsideClickAsync(object agrs)
        {
            if (!IsActive) return;

            await RunCloseDelayAsync();
        }

    }
}
 