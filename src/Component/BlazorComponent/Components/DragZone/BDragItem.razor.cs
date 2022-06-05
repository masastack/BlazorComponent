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

        public int Value { get { return DragZone.GetIndex(this); } }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public override void Dispose()
        {
            DragZone.Remove(this);
            base.Dispose();
        }
    }
}