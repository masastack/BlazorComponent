using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BItem : BDomComponentBase, IItem
    {
        [Parameter]
        public RenderFragment<ItemContext> ChildContent { get; set; }

        [CascadingParameter]
        public BItemGroup ItemGroup { get; set; }

        public bool IsActive { get; set; }

        [Parameter]
        public string Value { get; set; }

        public void Toggle()
        {
            IsActive = !IsActive;
            ItemGroup.NotifyItemChanged(this);
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
