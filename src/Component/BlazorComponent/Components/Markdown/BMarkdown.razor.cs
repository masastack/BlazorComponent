namespace BlazorComponent
{
    public partial class BMarkdown : BDomComponentBase, IMarkdown
    {
        [Parameter]
        public virtual string? Value { get; set; }
        [Parameter]
        public virtual string? Html { get; set; }
    }
}
