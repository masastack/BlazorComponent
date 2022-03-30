using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataFooterPaginationInfo<TComponent> where TComponent : IDataFooter
    {
        public DataPagination Pagination => Component.Pagination;

        public RenderFragment<(int PageStart, int PageStop, int ItemsLength)> PageTextContent => Component.PageTextContent;

        public string PageText => Component.PageText;
    }
}
