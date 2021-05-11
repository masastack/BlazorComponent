using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BImage : BDomComponentBase
    {
        protected string RespSizerStyle { get; set; }

        [Parameter] 
        public StringOrNumber AspectRatio { get; set; }

        [Parameter] 
        public bool Contain { get; set; }

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
        public string Src { get; set; }
    }
}