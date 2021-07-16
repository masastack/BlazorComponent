using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanelHeader : BDomComponentBase
    {
        [CascadingParameter]
        public BExpansionPanels ExpansionPanels { get; set; }

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
            ExpansionPanel.SetIsActive(!ExpansionPanel.Active);
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}
