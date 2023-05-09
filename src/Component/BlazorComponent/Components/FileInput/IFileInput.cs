using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public interface IFileInput<TValue> : ITextField<TValue>
    {
        RenderFragment<(int index, string text)>? SelectionContent { get; }

        IList<string?> Text { get; }

        bool HasChips { get; }

        InputFile? InputFile { get; set; }

        bool Multiple { get; }

        bool ShowSize { get; }

        StringNumber? ComputedCounterValue { get; }

        IList<IBrowserFile> Files { get; }

        void HandleOnFileChange(InputFileChangeEventArgs args);
    }
}
