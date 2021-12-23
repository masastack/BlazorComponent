using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BBreadcrumbs : BDomComponentBase, IBreadcrumbs, IBreadcrumbsDivider
    {
        protected string Tag { get; init; } = "ul";

        public bool RenderDivider { get; protected set; } = true;

        [Parameter]
        public string Divider { get; set; } = "/";

        [Parameter]
        public RenderFragment DividerContent { get; set; }

        [Parameter]
        public bool Linkage { get; set; }

        [Parameter]
        public IReadOnlyList<BreadcrumbItem> Items { get; set; } = new List<BreadcrumbItem>();

        [Parameter]
        public RenderFragment<BreadcrumbItem> ItemContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        #region When using razor definition without Items parameter

        internal List<BBreadcrumbsItem> SubBreadcrumbsItems { get; } = new();

        internal void AddSubBreadcrumbsItem(BBreadcrumbsItem item)
        {
            if (!SubBreadcrumbsItems.Contains(item))
            {
                SubBreadcrumbsItems.Add(item);
            }
        }

        #endregion
    }
}