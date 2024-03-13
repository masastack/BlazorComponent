namespace BlazorComponent
{
    public abstract partial class BContainer : BDomComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [MasaApiParameter("div")]
        public virtual string Tag { get; set; } = "div";
    }
}
