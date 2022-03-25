using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IOtpInput: IHasProviderComponent
    {

        string Type { get; set; }

        bool ReadOnly { get; set; }

        bool Disabled { get; set; }

        Task OnKeyUpAsync(BOtpInputKeyboardEventArgs args);

        Task OnInputAsync(BOtpInputChangeEventArgs args);

        Task OnPasteAsync(BOtpInputPasteWithDataEventArgs args);

        Task OnFocusAsync(int index);

        List<ElementReference> Elements { get; set; }

    }
}
