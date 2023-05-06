namespace BlazorComponent
{
    public partial class BExpansionPanelContent : BDomComponentBase
    {
        [CascadingParameter]
        public BExpansionPanel? ExpansionPanel { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
