using Microsoft.Extensions.Logging;

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

        [Inject]
        public ILogger<BDragZone> Logger { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

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

        [Parameter]
        public List<BDragItem> Value { get; set; }

        public List<BDragItem> Items { get; protected set; } = new();

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
            SetIndex();
        }

        public void AddRange(IEnumerable<BDragItem> sources, int position = -1)
        {
            AddRange(sources, Items, position);
            SetIndex();
        }

        public void Remove(params BDragItem[] items)
        {
            if (items == null || !items.Any())
                return;
            foreach (var item in items)
            {
                Remove(Items, item);
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

        public bool Update(string oldId, string newId)
        {
            var find = Items.FirstOrDefault(it => it.Id == oldId);
            if (find == null)
                return false;
            find.Id = newId;
            return true;
        }

        public bool Update(BDragItem item, int oldIndex, int newIndex)
        {
            var index = Items.FindIndex(it => it.Id == item.Id);
            if (index < 0)
                return false;
            if (index - newIndex == 0)
                return true;

            if (oldIndex - newIndex < 0)
            {
                Items.RemoveAt(oldIndex);
                if (newIndex - Items.Count == 0)
                {
                    Items = Items.Append(item).ToList();
                }
                else
                {
                    Items.Insert(newIndex, item);
                }
            }
            else
            {
                Items.RemoveAt(oldIndex);
                Items.Insert(newIndex, item);
            }
            SetIndex();
            return true;
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Logger.LogInformation($"OnAfterRenderAsync ids : { string.Join(",", Items.Select(m => m.Id))}");
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            Logger.LogInformation($"OnAfterRender ids : { string.Join(",", Items.Select(m => m.Id))}");
            base.OnAfterRender(firstRender);
        }

        private void AddRange(IEnumerable<BDragItem> sources, List<BDragItem> target, int position = -1)
        {
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

        protected void SetIndex()
        {
            //Logger.LogInformation($"SetIndex ids : { string.Join(",", Items.Select(m => m.Id))}");
            int index = 0, count = Items.Count;
            foreach (var item in Items)
            {
                if (item.Value - index != 0)
                    item.Value = index;
                index++;
            }


            //var total = 0;
            //while (count - index > 0)
            //{
            //    if (Items[index].Value - index != 0)
            //    {
            //        Items[index].Value = index;
            //        total++;
            //    }
            //    index++;
            //}
            StateHasChanged();
        }
    }
}