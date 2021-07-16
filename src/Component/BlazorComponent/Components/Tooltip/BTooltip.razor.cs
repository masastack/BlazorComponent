using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltip : BDomComponentBase
    {
        [CascadingParameter(Name = "Fixed")]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string ActivatorStyle { get; set; }

        [Obsolete("Use ActivatorContent instead.")]
        [Parameter]
        public RenderFragment Activator { get; set; }

        [Parameter]
        public RenderFragment ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public ElementReference ContentRef { get; set; }

        protected bool IsActive { get; set; }

        protected override void OnParametersSet()
        {
            if (Activator != null)
            {
                ActivatorContent = Activator;
            }
        }

        protected override void SetComponentClass()
        {
            CssProvider
              .AsProvider<BTooltip>()
              .Apply("activator", styleAction: s => s.Add(ActivatorStyle));
        }

        protected virtual Task OnMouseEnter()
        {
            IsActive = true;
            return Task.CompletedTask;
        }

        protected virtual void OnMouseLeave()
        {
            IsActive = false;
        }

        protected virtual void HandleOnClick()
        {
            IsActive = false;
        }
    }
}
