namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase, IDisposable
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        //protected override void OnWatcherInitialized()
        //{
        //    Watcher
        //        .Watch<string>(nameof(Id), val =>
        //        {
        //            if (!EqualityComparer<string>.Default.Equals(val, Id))
        //            {
        //                Id = val;
        //            }
        //        });
        //}

        protected override void OnAfterRender(bool firstRender)
        {
            //if (firstRender)
            //{
            //}
            DragZone.Register(this);
            base.OnAfterRender(firstRender);
        }

        [CascadingParameter]
        public BDragZone DragZone { get; set; }

        public int Value { get { return DragZone.GetIndex(this); } }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public BDragItem Clone()
        {
            return MemberwiseClone() as BDragItem;
        }

        public override void Dispose()
        {
            DragZone.Remove(this);
            base.Dispose();
        }
    }
}