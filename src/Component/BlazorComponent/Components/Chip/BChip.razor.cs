using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BChip : BDomComponentBase
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

        //[Parameter]
        //public bool IsActive { get; set; }

        public bool IsActive => ItemGroup != null && ItemGroup.Values.Contains(Value);

        [Parameter]
        public StringNumber Value { get; set; }

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
            //if (ItemGroup != null)
            //{
            //    IsActive = !IsActive;
            //    ItemGroup.NotifyItemChanged(this);
            //}

            if (ItemGroup != null)
            {
                await ItemGroup.TogglePanel(Value);
            }

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }

        //public void Active()
        //{
        //    if (IsActive)
        //    {
        //        return;
        //    }

        //    IsActive = true;
        //    StateHasChanged();
        //}

        //public void DeActive()
        //{
        //    if (!IsActive)
        //    {
        //        return;
        //    }

        //    IsActive = false;
        //    StateHasChanged();
        //}

        protected override void OnInitialized()
        {
            //if (ItemGroup != null)
            //{
            //    ItemGroup.AddItem(this);
            //}

            if (ItemGroup == null) return;

            if (Value == null)
            {
                Value = ItemGroup.AllKeys.Count;
            }

            ItemGroup.AllKeys.Add(Value);
        }

    }
}
