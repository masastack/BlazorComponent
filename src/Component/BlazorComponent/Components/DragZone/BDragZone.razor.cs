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
        public BDragDropService DragDropService { get; set; }

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

        public List<BDragItem> Items { get; protected set; } = new();

        public List<BDragItem> DynicItems { get; protected set; } = new();

        public void Register(BDragItem item)
        {
            if (!Contains(item, Items))
            {
                Add(Items, item, Items.Count);
                SetIndex();
            }
        }

        public void Add(BDragItem item, int position = -1)
        {
            Add(Items, item, position);
            Add(DynicItems, item);
            SetIndex();
        }

        public void AddRange(IEnumerable<BDragItem> sources, int position = -1)
        {
            AddRange(sources, Items, position);
            AddRange(sources, DynicItems, position);
            SetIndex();
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
            SetIndex();
        }

        public bool Contains(BDragItem item, List<BDragItem> list)
        {
            if (item == null || string.IsNullOrEmpty(item.Id))
                return true;
            return list.Any(it => it.Id == item.Id);
        }

        public int GetIndex(BDragItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id))
                return -1;
            return Items.FindIndex(it => it.Id == item.Id);
        }

        public bool Update(BDragItem item, int oldIndex, int newIndex)
        {
            if (oldIndex - newIndex < 0)
            {

                if (newIndex - Items.Count == 0)
                {
                    Items.Append(item);
                }
                else
                {
                    Items.Insert(newIndex + 1, item);
                }
                Items.RemoveAt(oldIndex);
            }
            else
            {
                Items.RemoveAt(oldIndex);
                Items.Insert(newIndex, item);
            }

            SetIndex();
            return true;
        }

        private void AddRange(IEnumerable<BDragItem> sources, List<BDragItem> target, int position = -1)
        {
            //int total = target.Count;
            //if (position < 0)
            //    position = 0;
            //if (position - total > 0)
            //    position = total;
            //target.InsertRange(position, sources);
            target.AddRange(sources);
        }

        private bool Add(List<BDragItem> list, BDragItem item, int position = -1)
        {
            if (item == null)
                return false;

            if (position >= 0 && position - list.Count < 0)
                list.Insert(position, item);
            else
                list.Add(item);
            return true;
        }

        private bool Remove(List<BDragItem> list, BDragItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id) || !list.Any())
                return false;
            var index = list.FindIndex(it => it.Id == item.Id);
            if (index < 0)
                return false;

            list.RemoveAt(index);
            return true;
        }

        private void SetIndex()
        {
            StateHasChanged();
            //int index = 0, count = Items.Count;
            //while (count - index > 0)
            //{
            //    if (Items[index].Value - index != 0)
            //        Items[index].Value = index;
            //    index++;
            //}
        }
    }
}