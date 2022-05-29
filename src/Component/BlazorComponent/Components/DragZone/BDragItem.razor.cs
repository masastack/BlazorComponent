namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase
    {
        protected override void OnInitialized()
        {
            DragZone.Register(this);
            base.OnInitialized();
        }

        [CascadingParameter]
        public BDragZone DragZone { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}