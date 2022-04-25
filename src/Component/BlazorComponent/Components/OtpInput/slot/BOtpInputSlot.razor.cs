
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BOtpInputSlot<TOtpInput> : ComponentPartBase<TOtpInput> where TOtpInput : IOtpInput
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public string Value { get; set; }

        public OtpInputType Type => Component.Type;

        public bool ReadOnly => Component.Readonly;

        public bool Disabled => Component.Disabled;

        public List<ElementReference> InputRefs => Component.InputRefs;

        public EventCallback<BOtpInputEventArgs<PasteWithDataEventArgs>> OnPaste => EventCallback.Factory.Create<BOtpInputEventArgs<PasteWithDataEventArgs>>(Component, Component.OnPasteAsync);

        public async Task OnPasteWithDataAsync(PasteWithDataEventArgs args)
        {
            if (OnPaste.HasDelegate)
            {
                await OnPaste.InvokeAsync(new BOtpInputEventArgs<PasteWithDataEventArgs>(args));
            }
        }
    }
}
