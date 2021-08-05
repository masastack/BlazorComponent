using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChip : BGroupItem<BItemGroup>
    {
        protected bool Show { get; set; } = true;

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string CloseIcon { get; set; }

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

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            await ToggleItem();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }
    }
}
