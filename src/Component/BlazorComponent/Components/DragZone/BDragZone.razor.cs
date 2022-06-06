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
            //Add(DynicItems, item);
            SetIndex();
        }

        public void AddRange(IEnumerable<BDragItem> sources, int position = -1)
        {
            AddRange(sources, Items, position);
            //AddRange(sources, DynicItems, position);
            SetIndex();
        }

        public void Remove(params BDragItem[] items)
        {
            if (items == null || !items.Any())
                return;
            foreach (var item in items)
            {
                Remove(Items, item);
                //Remove(DynicItems, item);
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

            Logger.LogInformation($"update start ids : { string.Join(",", Items.Select(m => m.Id))}");
            //var list = Items.ToArray().ToList();
            ////Items.Clear();
            //list[oldIndex] = null;
            //list.RemoveAt(oldIndex);

            //bool isAppend = false;
            //if (newIndex - oldIndex > 0)
            //{
            //    newIndex--;
            //    isAppend = true;
            //}

            //if (isAppend && list.Count - newIndex - 1 == 0)
            //    list.Add(item);
            //else
            //    list.Insert(newIndex, item);

            //Items = list;

           
            //var array = Items.ToArray();
            //array[oldIndex] = null;
            //array.r





            //var array = Items.ToArray();
            //var temp = array[newIndex];
            //array[oldIndex] = null;

            //int index = 0, count = array.Length;

            //while (count - index > 0)
            //{ 
            //    if()
            //}






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
            Logger.LogInformation($"update end ids : { string.Join(",", Items.Select(m => m.Id))}");
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
            //Logger.LogInformation($"SetIndex ids : { string.Join(",", Items.Select(m => m.Id))}");
            //int index = 0, count = Items.Count;
            //while (count - index > 0)
            //{
            //    if (Items[index].Value - index != 0)
            //        Items[index].Value = index;
            //    index++;
            //}
            //StateHasChanged();
        }
    }
}