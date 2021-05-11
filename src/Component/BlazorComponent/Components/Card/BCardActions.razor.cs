using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BCardActions : BDomComponentBase
    {
        [Parameter] 
        public  RenderFragment ChildContent { get; set; }
    }
}