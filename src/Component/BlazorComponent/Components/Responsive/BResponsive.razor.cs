using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BResponsive : BDomComponentBase, IResponsive
    {
        [Parameter] 
        public StringNumber AspectRatio { get; set; }

        [Parameter] 
        public string ContentClass { get; set; }

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
        public RenderFragment ChildContent { get; set; }
    }
}