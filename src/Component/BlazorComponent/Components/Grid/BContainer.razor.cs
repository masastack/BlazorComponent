namespace BlazorComponent
{
    public abstract partial class BContainer : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [MassApiParameter("div")]
        public virtual string Tag { get; set; } = "div";
    }
}
