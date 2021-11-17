
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public abstract partial class BVirtualScroll<TItem>
    {
        [Parameter]
        public ICollection<TItem> Items { get; set; }

        [Parameter]
        public RenderFragment<TItem> ItemContent { get; set; }

        [Parameter]
        public float ItemSize { get; set; } = 50;

        [Parameter]
        public int OverscanCount { get; set; } = 3;
    }
}
