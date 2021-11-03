using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChip
    {
        public BChip() : base(GroupType.ChipGroup)
        {
        }

        [Parameter]
        public bool Active { get; set; } = true;

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string CloseIcon { get; set; }

        [Parameter]
        public string CloseLabel { get; set; } = "Close";

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "span";

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            if (Matched)
            {
                (ItemGroup as BSlideGroup).SetWidths();
            }
            
            await ToggleItem();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}
