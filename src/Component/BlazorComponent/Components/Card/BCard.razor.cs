using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BCard : BDomComponentBase
    {
        [Parameter] 
        public StringNumber Height { get; set; }

        [Parameter] 
        public StringNumber MaxHeight { get; set; }

        [Parameter] 
        public StringNumber MinHeight { get; set; }

        [Parameter] 
        public StringNumber Width { get; set; }

        [Parameter] 
        public StringNumber MaxWidth { get; set; }

        [Parameter] 
        public StringNumber MinWidth { get; set; }

        [Parameter] 
        public bool Outlined { get; set; }

        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> Click{ get; set; }
    }
}