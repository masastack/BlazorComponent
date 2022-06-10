namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            DragZone.Register(this);
        }

        protected override void OnAfterRender(bool firstRender)
        {            
            base.OnAfterRender(firstRender);
        }

        [CascadingParameter]
        public BDragZone DragZone { get; set; }

        //[Parameter]
        public int Value { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public BDragItem Clone()
        {
            return MemberwiseClone() as BDragItem;
        }

        public override void Dispose()
        {
            //DragZone.Remove(this);
            base.Dispose();
        }
    }
}