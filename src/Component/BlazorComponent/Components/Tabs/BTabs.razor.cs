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
        protected List<BTab> Tabs { get; set; } = new();

        protected List<ITabItem> TabItems { get; set; } = new();

        protected BTab ActiveTab { get; set; }

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

        public void AddTab(BTab tab)
        {
            if (Tabs.Count == 0)
            {
                ActiveTab = tab;
                tab.Active();
            }

            Tabs.Add(tab);
        }

        public void AddTabItem(ITabItem tabItem)
        {
            if (TabItems.Count == 0)
            {
                tabItem.Active();
            }

            TabItems.Add(tabItem);
        }

        public void SelectTab(BTab selectedTab)
        {
            ActiveTab = selectedTab;

            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Tabs[i] == selectedTab)
                {
                    Tabs[i].Active();
                    TabItems[i].Active();
                }
                else
                {
                    Tabs[i].DeActive();
                    TabItems[i].DeActive();
                }
            }

            InvokeStateHasChanged();
        }

        public void Refresh()
        {
            StateHasChanged();
        }
    }
}
