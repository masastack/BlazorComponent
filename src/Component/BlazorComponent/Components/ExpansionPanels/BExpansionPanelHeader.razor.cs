using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanelHeader : BDomComponentBase
    {
        protected bool _hasMouseDown = false;

        [CascadingParameter]
        public BExpansionPanel ExpansionPanel { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Obsolete("Use OnClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void OnParametersSet()
        {
            if (Click.HasDelegate)
            {
                OnClick = Click;
            }
        }

        protected virtual async Task HandleClickAsync(MouseEventArgs args)
        {
            await JsInvokeAsync(JsInteropConstants.Blur, Ref);

            await ExpansionPanel.Toggle();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}
