using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPicker : BDomComponentBase
    {
        [Parameter]
        public RenderFragment TitleContent { get; set; }

        [Parameter]
        public RenderFragment ActionsContent { get; set; }
    }
}
