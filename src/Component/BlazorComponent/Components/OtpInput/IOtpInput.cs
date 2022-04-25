using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IOtpInput : IHasProviderComponent
    {
        OtpInputType Type { get; set; }

        bool Readonly { get; set; }

        bool Disabled { get; set; }

        Task OnPasteAsync(BOtpInputEventArgs<PasteWithDataEventArgs> args);

        List<ElementReference> InputRefs { get; set; }
    }
}
