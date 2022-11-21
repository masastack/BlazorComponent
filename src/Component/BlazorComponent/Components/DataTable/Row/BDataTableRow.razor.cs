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

        private bool _expanded;
        private bool _visible;
        private List<BDataTableRow<TItem>> _childDataTableRows = new();

        private int FirstCellIndex
        {
            get
            {
                int index = 0;
                var first = Headers.ElementAtOrDefault(0);
                if (first is { Value: "data-table-select" } or { Value: "data-table-expand" })
                {
                    index++;
                }

                var second = Headers.ElementAtOrDefault(1);
                if (second is { Value: "data-table-select" } or { Value: "data-table-expand" })
                {
                    index++;
                }

                return index;
            }
        }

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

            if (firstRender && ParentDataTableRow != null && DataIterator.TruGetTreeItemStatus(Item, out var value))
            {
                (_visible, _expanded) = value;
                StateHasChanged();
            }
        }

        internal void Register(BDataTableRow<TItem> row)
        {
            _childDataTableRows.Add(row);
        }

        internal void ToggleChildren(bool visible)
        {
            _visible = visible;
            DataIterator.UpdateTreeItemStatus(Item, (_visible, _expanded));

            if (visible)
            {
                if (_expanded)
                {
                    _childDataTableRows.ForEach(c => c.ToggleChildren(true));
                }
            }
            else
            {
                _childDataTableRows.ForEach(c => c.ToggleChildren(false));
            }
        }

        private void ToggleExpand()
        {
            _expanded = !_expanded;
            DataIterator.UpdateTreeItemStatus(Item, (_visible, _expanded));
            _childDataTableRows.ForEach(c => c.ToggleChildren(_expanded));
        }
    }
}
