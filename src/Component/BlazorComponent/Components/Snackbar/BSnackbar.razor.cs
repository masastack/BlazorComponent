using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSnackbar : BDomComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Action { get; set; }

        [Parameter]
        public RenderFragment ActionContent { get; set; }
    }
}
