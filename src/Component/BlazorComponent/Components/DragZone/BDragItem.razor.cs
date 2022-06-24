namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase, IDisposable
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

        public int Value
        {
            get
            {
                if (DragZone != null)
                    return DragZone.GetIndex(this);
                return -1;
            }
        }

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