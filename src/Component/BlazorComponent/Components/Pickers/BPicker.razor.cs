using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPicker : BDomComponentBase
    {
        [Parameter]
        public RenderFragment TitleContent { get; set; }

        [Parameter]
        public RenderFragment ActionsContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Type { get; set; } = "date";

        public string ActivePicker => Type.ToUpper();

        [Parameter]
        public bool NoTitle { get; set; }

        [Parameter]
        public string ActionsStyle { get; set; }
    }
}
