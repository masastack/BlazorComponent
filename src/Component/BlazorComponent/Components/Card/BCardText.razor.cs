using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BCardText : BDomComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}