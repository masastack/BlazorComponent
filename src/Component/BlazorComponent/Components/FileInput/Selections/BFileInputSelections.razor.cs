using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public partial class BFileInputSelections<TValue, TInput> where TInput : IFileInput<TValue>
    {
        public RenderFragment<(int index, string text)>? SelectionContent => Component.SelectionContent;

        public IList<IBrowserFile> Files => Component.Files;

        public IList<string> Text => Component.Text;

        public bool IsDirty => Component.IsDirty;

        public bool HasChips => Component.HasChips;
    }
}
