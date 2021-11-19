using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BlazorComponent
{
    public partial class BOverlay : BDomComponentBase
    {
        /// <summary>
        /// Controls whether the component is visible or hidden.
        /// </summary>
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        private void HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(args);
            }
        }
    }
}
