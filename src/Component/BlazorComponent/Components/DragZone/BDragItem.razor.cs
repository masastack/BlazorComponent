namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase
    {
        [CascadingParameter]
        public BDragZone DragZone { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DragZone.Register(this);
        }

        public BDragItem Clone()
        {
            var item = MemberwiseClone() as BDragItem;
            item.Id = Guid.NewGuid().ToString();
            return item;
        }
    }
}