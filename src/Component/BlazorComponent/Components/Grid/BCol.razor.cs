namespace BlazorComponent
{
    public abstract partial class BCol : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public StringNumber? Cols { get; set; }

        [Parameter]
        public StringNumber? Order { get; set; }

        [Parameter]
        public StringNumber? Offset { get; set; }

        [Parameter]
        [ApiDefaultValue("div")]
        public virtual string Tag { get; set; } = "div";
    }
}
