using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTab : BDomComponentBase
    {
        protected bool IsActive { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public BTabs Tabs { get; set; }

        public HtmlElement Rect { get; private set; }

        protected override void OnInitialized()
        {
            Tabs.AddTab(this);
        }

        public void HandleClick()
        {
            Tabs.SelectTab(this);
        }

        public void Active()
        {
            IsActive = true;
        }

        public void DeActive()
        {
            IsActive = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Rect = await JsInvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, Ref);
                Tabs.Refresh();
            }
        }
    }
}
