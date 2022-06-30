namespace BlazorComponent
{
    public partial class BDragZone : BDomComponentBase, IThemeable
    {
        [Inject]
        public BDragDropService DragDropService { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public List<BDragItem> Value { get; set; } = new();

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

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
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

            Value.RemoveAt(index);
            if (newIndex - Value.Count == 0)
                Value.Add(item);
            else
                Value.Insert(newIndex, item);
            UpdateIndex();
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

        private void UpdateIndex()
        {
            var index = 0;
            foreach (var item in Value)
            {
                if (item.Value - index != 0)
                    item.SetValue(index);
                index++;
            }
        }

        protected void FreshRender()
        {
            UpdateIndex();
            StateHasChanged();
        }
    }
}