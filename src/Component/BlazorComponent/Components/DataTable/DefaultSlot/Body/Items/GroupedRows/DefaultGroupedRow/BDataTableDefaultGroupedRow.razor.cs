namespace BlazorComponent
{
    public partial class BDataTableDefaultGroupedRow<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        [Parameter]
        public IGrouping<string, TItem> Group { get; set; } = null!;

        public IEnumerable<DataTableHeader<TItem>> Headers => Component.Headers;
        
        public RenderFragment? GroupHeaderContent => Component.GroupHeaderContent;

        public Dictionary<string, object?> ColspanAttrs => Component.ColspanAttrs;

        public Dictionary<string, bool> OpenCache => Component.OpenCache;

        public bool IsOpen
        {
            get
            {
                if (OpenCache.TryGetValue(Group.Key, out var isOpen))
                {
                    return isOpen;
                }

                return false;
            }
        }

        public string GroupMinusIcon => Component.GroupMinusIcon;

        public string GroupPlusIcon => Component.GroupPlusIcon;

        public string GroupCloseIcon => Component.GroupCloseIcon;

        public DataOptions Options => Component.Options;

        private string GetText(string value)
        {
            return Headers.FirstOrDefault(h => h.Value == value)?.Text ?? value;
        }
    }
}
