using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BToolbarBody<TToolbar> where TToolbar : IToolbar
    {
        public string Src => Component.Src;

        public RenderFragment<ImgProps> ImgContent => Component.ImgContent;

        public StringNumber Height => Component.Height;

        public bool IsExtended => Component.IsExtended;

        public RenderFragment ExtensionContent => Component.ExtensionContent;

        public ImgProps ImgProps => new()
        {
            Height = Height,
            Src = Src
        };
    }
}
