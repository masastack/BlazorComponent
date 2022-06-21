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

        public List<BDragItem> Value
        {
            get { return _value; }
            set
            {
                if (value == null || !value.Any())
                {
                    if (!_value.Any())
                        return;
                    _value.Clear();
                }
                else
                {
                    _value = value;
                }
            }
        }

        private List<BDragItem> _value = new();

        protected bool _isRender = true;

        protected override void OnParametersSet()
        {
            _isRender = true;
            base.OnParametersSet();
        }

        protected override Task OnParametersSetAsync()
        {
            _isRender = true;
            return base.OnParametersSetAsync();
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender() && _isRender;
        }

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

        public void Register(BDragItem item)
        {
            if (!Contains(item))
            {
                Add(Value, item);
                FreshRender();
            }
        }

        public void Add(BDragItem item, int position = -1)
        {
            Add(Value, item, position);
            FreshRender();
        }

        public void AddRange(IEnumerable<BDragItem> sources)
        {
            Value.AddRange(sources);
            FreshRender();
        }

        public void Remove(params BDragItem[] items)
        {
            if (items == null || !items.Any())
                return;
            foreach (var item in items)
            {
                Remove(Value, item);
            }
            FreshRender();
        }

        public void Clear()
        {
            Value.Clear();
            FreshRender();
        }

        public bool Contains(BDragItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id))
                return true;
            return Value.Any(it => it.Id == item.Id);
        }

        public int GetIndex(BDragItem item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id))
                return -1;
            return Value.FindIndex(it => it.Id == item.Id);
        }

        public bool Update(BDragItem item, int oldIndex, int newIndex)
        {
            var index = Value.FindIndex(it => it.Id == item.Id);
            if (index < 0)
                return false;
            if (index - newIndex == 0)
                return true;

            if (oldIndex - newIndex < 0)
            {
                int start = oldIndex, end = newIndex;
                do
                {
                    Value[start] = Value[start + 1];
                    start++;
                }
                while (end - start > 0);
                Value[end] = item;
            }
            else
            {
                int start = newIndex, end = oldIndex;
                do
                {
                    Value[end] = Value[end - 1];
                    end--;
                }
                while (end - start > 0);
                Value[start] = item;
            }
            FreshRender();
            return true;
        }

        private bool Add(List<BDragItem> list, BDragItem item, int position = -1)
        {
            if (item == null)
                return false;
            if (list.Any(it => it.Id == item.Id))
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

        protected void FreshRender()
        {
            StateHasChanged();
        }
    }
}