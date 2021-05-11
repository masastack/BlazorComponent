using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDialog : BDomComponentBase
    {
        [Parameter] 
        public bool Visible { get; set; }

        [Parameter] 
        public EventCallback<bool> VisibleChanged { get; set; }

        [Parameter] 
        public StringOrNumber Width { get; set; }

        [Parameter] 
        public StringOrNumber MaxWidth { get; set; }

        [Parameter] 
        public bool Persistent { get; set; }

        [Parameter] 
        public bool Scrollable { get; set; }

        [Parameter] 
        public RenderFragment ChildContent { get; set; }
    }
}