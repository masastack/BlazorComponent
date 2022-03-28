
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

        public EventCallback<BOtpInputEventArgs<KeyboardEventArgs>> OnKeyup => EventCallback.Factory.Create<BOtpInputEventArgs<KeyboardEventArgs>>(Component, Component.OnKeyUpAsync);

        public EventCallback<BOtpInputEventArgs<ChangeEventArgs>> OnInput => EventCallback.Factory.Create<BOtpInputEventArgs<ChangeEventArgs>>(Component, Component.OnInputAsync);

        public EventCallback<BOtpInputEventArgs<PasteWithDataEventArgs>> OnPaste => EventCallback.Factory.Create<BOtpInputEventArgs<PasteWithDataEventArgs>>(Component, Component.OnPasteAsync);

        public async Task OnKeyUpAsync(KeyboardEventArgs args, int index)
        {
            if (OnKeyup.HasDelegate)
            {
                await OnKeyup.InvokeAsync(new BOtpInputEventArgs<KeyboardEventArgs>(args, index));
            }
        }

        public async Task OnInputAsync(ChangeEventArgs args, int index)
        {
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(new BOtpInputEventArgs<ChangeEventArgs>(args, index));
            }
        }


        public async Task OnPasteWithDataAsync(PasteWithDataEventArgs args)
        {
            if (OnPaste.HasDelegate)
            {
                await OnPaste.InvokeAsync(new BOtpInputEventArgs<PasteWithDataEventArgs>(args));
            }
        }

        public EventCallback<int> OnFocus => EventCallback.Factory.Create<int>(Component, Component.OnFocusAsync);

        public async Task OnFocusAsync(int index)
        {
            if (OnFocus.HasDelegate)
            {
                await OnFocus.InvokeAsync(index);
            }
        }
    }
}
