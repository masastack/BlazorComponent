
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
        public IReadOnlyList<TItem> Items { get; set; }

        [Parameter]
        public RenderFragment<TItem> ItemContent { get; set; }

        protected abstract int FirstToRender { get; }

        protected abstract int LastToRender { get; }

        protected abstract void OnRenderItem(int index,TItem item);

        protected abstract void OnScroll(int scrollTop);

        private async Task OnScroll(EventArgs args)
        {
            var scrollTop = await JsInvokeAsync<int>("eval", $"document.getElementById('{Id}').scrollTop");
            OnScroll(scrollTop); 
        }
    }
}
