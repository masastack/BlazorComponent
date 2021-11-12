using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BBreadcrumbs<TItem> : BDomComponentBase, IBreadcrumbs<TItem> where TItem: BreadcrumbItem
    {
        [Parameter]
        public string Tag { get; set; } = "ul";

        [Parameter]
        public string Divider { get; set; } = "/";

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment DividerContent { get; set; }

        [Parameter]
        public RenderFragment<TItem> ItemContent { get; set; }
    }

    public class BreadcrumbItem
    {
        public bool Disabled { get; set; }

        public string Href { get; set; }

        public string Text { get; set; }

        public bool Exact { get; set; }

        public bool Link { get; set; }

        public string To { get; set; }
    }
}