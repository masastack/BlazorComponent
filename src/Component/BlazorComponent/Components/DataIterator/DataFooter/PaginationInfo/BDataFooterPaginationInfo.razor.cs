using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataFooterPaginationInfo<TComponent> where TComponent : IDataFooter
    {
        public DataPagination Pagination => Component.Pagination;

        public RenderFragment<(int PageStart, int PageStop, int ItemsLength)> PageTextContent => Component.PageTextContent;

        public string PageText => Component.PageText;
    }
}
