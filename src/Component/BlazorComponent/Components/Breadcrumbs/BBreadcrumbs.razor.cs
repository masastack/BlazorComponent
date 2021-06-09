using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BBreadcrumbs<TItem> : BDomComponentBase
    {
        [Parameter]
        public String Divider { get; set; }

        [Parameter]
        public bool DisabledLast { get; set; }

        [Parameter]
        public Func<TItem, string> ItemText { get; set; } = null!;
         
        [Parameter]
        public Func<TItem, string> ItemValue { get; set; } = null!;

        [Parameter]
        public string DisabledValue { get; set; } = null!;

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public RenderFragment ChildContent { get; set; }

    }
}
