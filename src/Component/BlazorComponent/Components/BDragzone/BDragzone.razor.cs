namespace BlazorComponent
{
    public partial class BDragzone<TItem> : BDomComponentBase, IThemeable
    {
        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public List<BDragItem<TItem>> Items { get; set; }

        [Parameter]
        public string ActiveClass { get; set; } = "dragItem-active";

        [Parameter]
        public Func<BDragItem<TItem>, bool> AllowDragFn { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

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

        public void OnDragStart(BDragItem<TItem> item)
        {
            if (!AllowSwap(item))
                return;

            if (dragDropService.DragItem == item)
                return;

            dragDropService.DragItem = item;
            dragDropService.DropRoot = this;
            if (dragDropService.DragItem.CssClass.IndexOf(ActiveClass) < 0)
                dragDropService.DragItem.CssClass = $"{dragDropService.DragItem.CssClass} {ActiveClass}";
        }

        public void OnDragEnd()
        {
            Reset();
        }

        public void OnDragEnter(BDragItem<TItem> item)
        {
            if (!AllowSwap(item))
                return;
            if (dragDropService.DragItem is null)
                return;
            if (dragDropService.DropItem == item)
                return;

            dragDropService.DropItem = item;

            if (dragDropService.DropRoot != this)
            {
                //if (dragDropService.DropRoot is not null)
                {
                    dragDropService.DropRoot.Items.Remove(dragDropService.DragItem);
                    dragDropService.DropRoot.StateHasChanged();
                }
                dragDropService.DropRoot = this;
            }

            int dragIndex = Items.IndexOf(dragDropService.DragItem), dropIndex = Items.IndexOf(dragDropService.DropItem);

            if (dragIndex >= 0)
            {
                Items[dragIndex] = Items[dropIndex];
                Items[dropIndex] = dragDropService.DragItem;
            }
            else
            {
                Items.Insert(dropIndex, dragDropService.DragItem);
            }

            StateHasChanged();
        }

        public void OnDragLeave()
        {
            dragDropService.DropItem = default;
        }

        public void OnDrop()
        {
            Reset(false);
        }

        private void Reset(bool isDragEnd = true, bool reset = true)
        {
            if (dragDropService.DragItem == null)
                return;

            if (isDragEnd)
            {
                bool isSelf = this == dragDropService.DropRoot;
                if (!isSelf)
                {
                    if (Items.Contains(dragDropService.DragItem))
                    {
                        Items.Remove(dragDropService.DragItem);
                        StateHasChanged();
                    }
                }

                if (dragDropService.DragItem.CssClass.Contains(ActiveClass, StringComparison.CurrentCulture))
                {
                    dragDropService.DragItem.CssClass = dragDropService.DragItem.CssClass.Replace(ActiveClass, "").Trim();
                    if (isSelf)
                        StateHasChanged();
                    else
                        dragDropService.DropRoot.StateHasChanged();
                }
                if (reset)
                    dragDropService.Reset();
            }
            else
            {
                if (dragDropService.DragItem.CssClass.Contains(ActiveClass, StringComparison.CurrentCulture))
                {
                    dragDropService.DragItem.CssClass = dragDropService.DragItem.CssClass.Replace(ActiveClass, "").Trim();

                    StateHasChanged();
                }
            }
        }

        private bool AllowSwap(BDragItem<TItem> item)
        {
            if (item == null || item.Item == null)
                return false;
            if (item.Fixed || !item.Enable)
                return false;
            if (AllowDragFn != null)
            {
                var t = AllowDragFn(item);
                if (!t)
                {

                }
                return t;
            }

            return true;
        }
    }
}