namespace BlazorComponent
{
    public partial class BDragZone : BDomComponentBase, IThemeable
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        [Inject]
        public BDragDropService<BDragItem> DragDropService { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool LoadItems { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        protected List<BDragItem> Items { get; set; } = new();

        protected List<BDragItem> DynicItems { get; set; } = new();        

        public void Register(BDragItem item)
        {
            Add(Items, item, Items.Count);
        }

        public void Add(BDragItem item, int position = -1)
        {
            Add(Items, item, position);
            Add(DynicItems, item, position);
        }

        public void AddRange(IEnumerable<BDragItem> sources, int position = -1)
        {
            AddRange(sources, Items, position);
            AddRange(sources, DynicItems, position);
        }

        public void Remove(params BDragItem[] items)
        {
            if (items == null || !items.Any())
                return;
            foreach (var item in items)
            {
                Remove(Items, item);
                Remove(DynicItems, item);
            }
        }

        private void AddRange(IEnumerable<BDragItem> sources, List<BDragItem> target, int position = -1)
        {
            int total = target.Count;
            if (position < 0)
                position = 0;
            if (position - total > 0)
                position = total;
            target.InsertRange(position, sources);
        }

        private bool Add(List<BDragItem> list, BDragItem item, int position = -1)
        {
            if (item == null)
                return false;

            int index = list.IndexOf(item), total = list.Count;
            if (position < 0)
                position = 0;
            if (position - total > 0)
                position = total;

            if (index < 0)
            {
                list.Insert(position, item);
            }
            else
            {
                if (position - total == 0)
                    position--;

                list.RemoveAt(index);
                list.Insert(position, item);
            }
            return true;
        }

        private bool Remove(List<BDragItem> list, BDragItem item)
        {
            if (item == null || !list.Any())
                return false;
            var index = list.IndexOf(item);
            if (index < 0)
                return false;

            list.RemoveAt(index);
            return true;
        }
    }
}