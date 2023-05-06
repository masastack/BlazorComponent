namespace BlazorComponent
{
    public partial class BToolbarBody<TToolbar> where TToolbar : IToolbar
    {
        public string Src => Component.Src;

        public RenderFragment<Dictionary<string, object>> ImgContent => Component.ImgContent;

        public StringNumber Height => Component.Height;

        public bool IsExtended => Component.IsExtended;

        public RenderFragment? ExtensionContent => Component.ExtensionContent;

        public Dictionary<string, object> ImgProps => new()
        {
            { "Height", Height },
            { "Src", Src }
        };
    }
}