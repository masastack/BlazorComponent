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

        //[Parameter]
        public int Value
        {
            get
            {
                if (DragZone != null)
                    return DragZone.GetIndex(this);
                return -1;
            }
            set
            {

            }
        }

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