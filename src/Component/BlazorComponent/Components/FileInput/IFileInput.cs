using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IFileInput<TValue> : ITextField<TValue>
    {
        RenderFragment<(int index, string text)> SelectionContent { get; }

        IList<string> Text { get; }

        bool HasChips { get; }

        InputFile InputFile { get; set; }

        bool Multiple { get; }

        bool ShowSize { get; }

        StringNumber ComputedCounterValue { get; }

        IList<IBrowserFile> Files { get; }

        Task HandleOnFileChangeAsync(InputFileChangeEventArgs args);
    }
}
