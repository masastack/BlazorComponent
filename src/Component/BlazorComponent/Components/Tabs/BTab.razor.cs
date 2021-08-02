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
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [CascadingParameter]
        public BTabs Tabs { get; set; }

        public HtmlElement Rect { get; private set; }

        [Parameter]
        public string Key { get; set; }

        public string ComputedKey => Key ?? (Tabs.Tabs.IndexOf(this) + 1).ToString();

        public bool IsActive => Tabs.Value == ComputedKey || (Tabs.Value == null && Tabs.Tabs.IndexOf(this) == 0);

        protected override void OnInitialized()
        {
            Tabs.AddTab(this);
        }

        public async Task HandleClick()
        {
            await Tabs.SelectTabAsync(this);
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
