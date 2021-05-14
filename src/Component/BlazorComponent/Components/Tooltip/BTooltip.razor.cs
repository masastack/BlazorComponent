using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTooltip : BDomComponentBase
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string ActivatorStyle { get; set; }

        [Parameter]
        public RenderFragment Activator { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public ElementReference ContentRef { get; set; }

        protected bool IsActive { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
              .AsProvider<BTooltip>()
              .Apply("activator", styleAction: s => s.Add(ActivatorStyle));
        }
    }
}
