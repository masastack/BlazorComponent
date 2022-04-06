using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BStepperHeader : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
