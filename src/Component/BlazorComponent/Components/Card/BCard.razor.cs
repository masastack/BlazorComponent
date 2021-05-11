using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BCard : BDomComponentBase
    {
        [Parameter] 
        public StringOrNumber Height { get; set; }

        [Parameter] 
        public StringOrNumber MaxHeight { get; set; }

        [Parameter] 
        public StringOrNumber MinHeight { get; set; }

        [Parameter] 
        public StringOrNumber Width { get; set; }

        [Parameter] 
        public StringOrNumber MaxWidth { get; set; }

        [Parameter] 
        public StringOrNumber MinWidth { get; set; }

        [Parameter] 
        public bool Outlined { get; set; }

        [Parameter] 
        public RenderFragment ChildContent { get; set; }
    }
}