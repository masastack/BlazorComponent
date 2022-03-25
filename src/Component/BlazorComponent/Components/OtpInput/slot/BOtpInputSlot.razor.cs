
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BOtpInputSlot<TOtpInput> : ComponentPartBase<TOtpInput> where TOtpInput : IOtpInput
    {

        public string Type => Component.Type;

        public bool ReadOnly => Component.ReadOnly;

        public bool Disabled => Component.Disabled;

        public List<ElementReference> Elements => Component.Elements;

        [Parameter]
        public int Index { get; set; }

        public EventCallback<BOtpInputKeyboardEventArgs> OnKeyup => EventCallback.Factory.Create<BOtpInputKeyboardEventArgs>(Component, Component.OnKeyUpAsync);

        public EventCallback<BOtpInputChangeEventArgs> OnInput => EventCallback.Factory.Create<BOtpInputChangeEventArgs>(Component, Component.OnInputAsync);

        public EventCallback<BOtpInputPasteWithDataEventArgs> OnPaste => EventCallback.Factory.Create<BOtpInputPasteWithDataEventArgs>(Component, Component.OnPasteAsync);

        [CascadingParameter]
        public List<string> Values { get; set; }

        [Parameter]
        public string Value { get;set; }

        public async Task OnKeyUpAsync(KeyboardEventArgs args, int index)
        {
            if (OnKeyup.HasDelegate)
            {
                await OnKeyup.InvokeAsync(new BOtpInputKeyboardEventArgs() { Args = args, Index = index });
            }
        }

        public async Task OnInputAsync(ChangeEventArgs args, int index)
        {
            if (OnInput.HasDelegate)
            {
                await OnInput.InvokeAsync(new BOtpInputChangeEventArgs() { Args = args, Index = index });
            }
        }


        public async Task OnPasteWithDataAsync(PasteWithDataEventArgs args)
        {
            if (OnPaste.HasDelegate)
            {
                await OnPaste.InvokeAsync(new BOtpInputPasteWithDataEventArgs() { Args = args, Index = Index });
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
