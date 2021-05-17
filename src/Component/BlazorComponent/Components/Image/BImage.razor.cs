using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BImage : BDomComponentBase
    {
        protected string RespSizerStyle { get; set; }

        [Parameter] 
        public StringNumber AspectRatio { get; set; }

        [Parameter] 
        public bool Contain { get; set; }

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
        public string Src { get; set; }
    }
}