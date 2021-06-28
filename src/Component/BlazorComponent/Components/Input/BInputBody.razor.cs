using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputBody : BDomComponentBase, IInputBody
    {
        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public MarkupString InnerHtml { get; set; } = (MarkupString)"&ZeroWidthSpace;";

        [Parameter]
        public bool ShowLabel { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public bool IsFocused { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public InputContext InputContext { get; set; } = new();

        [Parameter]
        public string PlaceHolder { get; set; }

        [Parameter]
        public string Type { get; set; } = "text";

        [Parameter]
        public bool IsTextArea { get; set; }

        [Parameter]
        public bool IsTextField { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Rows { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public RenderFragment PrependInnerContent { get; set; }

        public ElementReference InputRef { get; set; }

        public Task HandleChangeAsync(ChangeEventArgs args)
        {
            InputContext.NotifyValueChanged(args.Value.ToString());
            return Task.CompletedTask;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                InputContext.InputRef = InputRef;
            }
        }
    }
}
