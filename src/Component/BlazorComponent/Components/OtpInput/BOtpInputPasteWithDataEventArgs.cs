
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public class BOtpInputPasteWithDataEventArgs
    {
        public PasteWithDataEventArgs Args { get; set; }
        
        public int Index { get; set; }
    }
}
