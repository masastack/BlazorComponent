using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChip : BDomComponentBase, IItem
    {
        protected bool Show { get; set; } = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string CloseIcon { get; set; }

        [CascadingParameter]
        public BItemGroup ItemGroup { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            if (ItemGroup != null)
            {
                IsActive = !IsActive;
                ItemGroup.NotifyItemChanged(this);
            }


            if (Click.HasDelegate)
            {
                await Click.InvokeAsync();
            }
        }

        public void Active()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            StateHasChanged();
        }

        public void DeActive()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            if (ItemGroup != null)
            {
                ItemGroup.AddItem(this);
            }
        }

    }
}
