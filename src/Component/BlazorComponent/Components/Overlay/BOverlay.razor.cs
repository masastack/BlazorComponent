using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BlazorComponent
{
    public partial class BOverlay : BDomComponentBase
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public string Color { get; set; } = "#212121";

        [Parameter]
        public StringNumber Opacity { get; set; } = 0.46;

        /// <summary>
        /// Controls whether the component is visible or hidden.
        /// </summary>
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public int ZIndex { get; set; } = 201;

        [Obsolete("Use OnClick instead.")]
        [Parameter]
        public EventCallback<MouseEventArgs> Click { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

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
            else
            {
                ValueChanged.InvokeAsync(false);
            }
        }
    }
}
