using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BCardTitle : BDomComponentBase
    {
        [Parameter]
        [NotNull]
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            ArgumentNullException.ThrowIfNull(ChildContent);
        }
    }
}