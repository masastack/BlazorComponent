namespace BlazorComponent
{
    public partial class BDataTableFooters<TItem, TDataTable> where TDataTable : IDataTable<TItem>
    {
        public RenderFragment? FooterContent => Component.FooterContent;

        public bool HideDefaultFooter => Component.HideDefaultFooter;
    }
}
