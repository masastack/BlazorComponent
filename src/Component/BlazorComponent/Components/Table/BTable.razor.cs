using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTable<TItem> : BDomComponentBase, ITable
    {
        [Parameter]
        public string Align { get; set; } = "start";

        [Parameter]
        public RenderFragment<TItem> ChildContent { get; set; }

        [Obsolete("Use TopContent instead.")]
        [Parameter]
        public RenderFragment Top { get; set; }

        [Parameter]
        public RenderFragment TopContent { get; set; }

        [Obsolete("Use TopContent instead.")]
        [Parameter]
        public RenderFragment Bottom { get; set; }

        [Parameter]
        public RenderFragment BottomContent { get; set; }

        [Obsolete("Use NoResultContent instead.")]
        [Parameter]
        public RenderFragment NoResult { get; set; }

        [Parameter]
        public RenderFragment NoResultContent { get; set; }

        [Parameter]
        public bool HideDefaultHeader { get; set; }

        [Parameter]
        public bool HideDefaultFooter { get; set; }

        [Parameter]
        public List<TableHeaderOptions> Headers { get; set; }

        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public IEnumerable<TItem> Items { get; set; }

        [Parameter]
        public bool Stripe { get; set; }

        [Parameter]
        public string TableLayout { get; set; } = "auto";

        public ElementReference WrapRef { get; set; }

        protected int PageStart => ((Page - 1) * PageSize) + 1;

        protected int PageStop => Page == TotalPage ? TotalCount : Page * PageSize;

        [Parameter]
        public int Page { get; set; } = 1;

        [Parameter]
        public int PageSize { get; set; } = 10;

        protected int TotalPage => Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDecimal(PageSize)));

        protected int TotalCount => Items?.Count() ?? 0;

        protected bool PrevDisabled => Page <= 1;

        protected bool NextDisabled => Page >= TotalPage;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Top != null)
            {
                TopContent = Top;
            }

            if (Bottom != null)
            {
                BottomContent = Bottom;
            }

            if (NoResult != null)
            {
                NoResultContent = NoResult;
            }
        }

        public virtual Task HandleScrollAsync(EventArgs args)
        {
            return Task.CompletedTask;
        }

        void ITable.SetTableLayoutFixed()
        {
            TableLayout = "fixed";
            StateHasChanged();
        }
    }
}