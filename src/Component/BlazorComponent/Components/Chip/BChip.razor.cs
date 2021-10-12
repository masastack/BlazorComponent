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

        protected bool Show { get; set; } = true;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            if (Matched)
            {
                (ItemGroup as BSlideGroup).SetWidths();
            }
            
            await ToggleItem();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }
    }
}
