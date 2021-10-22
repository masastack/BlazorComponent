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

        private void HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                OnClick.InvokeAsync(args);
            }
        }
    }
}
