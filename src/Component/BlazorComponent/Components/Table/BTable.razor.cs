using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Components.Core.CssProcess;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTable<TItem> : BDomComponentBase
    {
        [Parameter]
        public RenderFragment<TItem> ChildContent { get; set; }

        [Parameter]
        public RenderFragment Top { get; set; }

        [Parameter]
        public RenderFragment Bottom { get; set; }

        [Parameter]
        public RenderFragment NoResult { get; set; }

        [Parameter]
        public bool HideDefaultHeader { get; set; }

        [Parameter]
        public bool HideDefaultFooter { get; set; }

        [Parameter]
        public List<string> Headers { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public IEnumerable<TItem> Items { get; set; }

        protected int PageStart => ((Page - 1) * PageSize) + 1;

        protected int PageStop => Page == TotalPage ? TotalCount : Page * PageSize;

        protected int Page { get; set; } = 1;

        protected int PageSize { get; set; } = 10;

        protected int TotalPage => Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDecimal(PageSize)));

        protected int TotalCount => Items?.Count() ?? 0;

        protected bool PrevDisabled => Page <= 1;

        protected bool NextDisabled => Page >= TotalPage;
    }
}
