namespace BlazorComponent
{
    public partial class BDataTableHeaders<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment? HeaderContent => Component.HeaderContent;

        public bool HideDefaultHeader => Component.HideDefaultHeader;

        public StringBoolean? Loading => Component.Loading;

        public bool IsMobile => Component.IsMobile;
    }
}
