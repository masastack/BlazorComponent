using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTabs : BDomComponentBase
    {
        public List<BTab> Tabs { get; set; } = new();

        public List<ITabItem> TabItems { get; set; } = new();

        protected BTab ActiveTab => Tabs.Find(r => r.IsActive);

        [Parameter]
        public bool HideSlider { get; set; }

        [Parameter]
        public RenderFragment SliderContent { get; set; }

        [Parameter]
        public RenderFragment TabContent { get; set; }

        [Parameter]
        public RenderFragment ItemsContent { get; set; }

        [Parameter]
        public RenderFragment ItemContent { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        public void AddTab(BTab tab)
        {
            Tabs.Add(tab);
        }

        public void AddTabItem(ITabItem tabItem)
        {
            TabItems.Add(tabItem);
        }

        public async Task SelectTabAsync(BTab selectedTab)
        {
            Value = selectedTab.ComputedKey;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }

            InvokeStateHasChanged();
        }

        public void Refresh()
        {
            StateHasChanged();
        }
    }
}
