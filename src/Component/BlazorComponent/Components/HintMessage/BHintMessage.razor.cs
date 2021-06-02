using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BHintMessage : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}