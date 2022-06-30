namespace BlazorComponent
{
    public partial class BDragItem : BDomComponentBase, IDisposable, IComparable<BDragItem>
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

        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }

        
        public int Value { get; private set; }

        internal void SetValue(int val) {
            Value = val;
        }

        public BDragItem Clone()
        {
            return MemberwiseClone() as BDragItem;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public int CompareTo(BDragItem other)
        {
            return this.Value - other.Value;
        }
    }
}