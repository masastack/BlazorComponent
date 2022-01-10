using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanelHeader : BDomComponentBase
    {
        protected bool HasMouseDown;

        protected bool IsActive => ExpansionPanel?.InternalIsActive ?? false;

        protected bool IsDisabled => ExpansionPanel?.IsDisabled ?? false;

        protected bool IsReadonly => ExpansionPanel?.IsReadonly ?? false;

        [CascadingParameter]
        public BExpansionPanel ExpansionPanel { get; set; }

        [Parameter]
        public RenderFragment ActionsContent { get; set; }

        [Parameter]
        public RenderFragment<bool> ChildContent { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public bool DisableIconRotate { get; set; }

        [Parameter]
        public string ExpandIcon { get; set; }

        [Parameter]
        public bool HideActions { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected virtual async Task HandleClickAsync(MouseEventArgs args)
        {
            await JsInvokeAsync(JsInteropConstants.Blur, Ref);

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }

            if (!(IsReadonly || IsDisabled))
            {
                await ExpansionPanel.Toggle();
            }
        }
    }
}