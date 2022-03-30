using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataFooter
    {
        [Parameter]
        public RenderFragment PrependContent { get; set; }
    }
}
