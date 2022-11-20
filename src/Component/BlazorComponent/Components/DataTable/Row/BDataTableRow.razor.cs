using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableRow<TItem>
    {
        [CascadingParameter]
        protected BDataIterator<TItem> DataIterator { get; set; }

        [CascadingParameter]
        protected BDataTableRow<TItem>? ParentDataTableRow { get; set; }

        [Parameter]
        public List<DataTableHeader<TItem>> Headers { get; set; }

        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public Func<TItem, List<TItem>> ItemChildren { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public Func<ItemColProps<TItem>, bool> HasSlot { get; set; }

        [Parameter]
        public RenderFragment<ItemColProps<TItem>> SlotContent { get; set; }

        [Parameter]
        public int Level { get; set; }

        internal bool _expand;
        private bool _booted;
        private bool _visible = false;
        private List<BDataTableRow<TItem>> _childDataTableRows = new();

        protected List<TItem> Children => ItemChildren?.Invoke(Item) ?? new();

        protected virtual string TreeOpenIcon { get; set; }

        protected virtual string TreeCloseIcon { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (ParentDataTableRow is not null)
            {
                ParentDataTableRow.Register(this);
            }
            else
            {
                _visible = true;
            }
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender && ParentDataTableRow != null && DataIterator.TryGetExpand(Item, out var value))
            {
                (_visible, _expand) = value;
                StateHasChanged();
            }
        }

        internal void Register(BDataTableRow<TItem> row)
        {
            _childDataTableRows.Add(row);
        }

        internal void ToggleChildren(bool visible)
        {
            if (visible == false)
            {
                _visible = false;
                _childDataTableRows.ForEach(c => c.ToggleChildren(false));
                DataIterator.UpdateExpand(Item, (false, _expand));
            }
            else
            {
                _visible = true;

                if (_expand)
                {
                    _childDataTableRows.ForEach(c => c.ToggleChildren(true));
                }

                DataIterator.UpdateExpand(Item, (true, _expand));
            }
        }

        private void ToggleExpand()
        {
            _expand = !_expand;
            DataIterator.UpdateExpand(Item, (true, _expand));
            _childDataTableRows.ForEach(c => c.ToggleChildren(_expand));
        }
    }
}
