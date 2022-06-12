namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            DragZone.Register(this);
        }       

        [CascadingParameter]
        public BDragZone DragZone { get; set; }
        
        public int Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public BDragItem Clone()
        {
            return MemberwiseClone() as BDragItem;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}