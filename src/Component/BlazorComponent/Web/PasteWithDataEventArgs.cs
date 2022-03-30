using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Web
{
    public class PasteWithDataEventArgs : ClipboardEventArgs
    {
        public string PastedData { get; set; }
    }
}
