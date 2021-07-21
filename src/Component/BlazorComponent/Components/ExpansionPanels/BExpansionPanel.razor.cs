using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BExpansionPanel : BDomComponentBase
    {
        private bool _disabled;

        public bool Expanded => ExpansionPanels.Values.Contains(Key);

        public bool NextActive => ExpansionPanels.NextActiveKeys.Contains(Key);

        [CascadingParameter]
        public BExpansionPanels ExpansionPanels { get; set; }

        [Parameter]
        public StringNumber Key { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Disabled
        {
            get => ExpansionPanels.Disabled || _disabled;
            set => _disabled = value;
        }

        public async Task Toggle()
        {
            await ExpansionPanels.TogglePanel(Key);
        }

        protected override Task OnInitializedAsync()
        {
            // 当没有设置key时自动赋值
            // TODO: 可能不一定按预期一样顺序赋值
            if (Key == null)
                Key = ExpansionPanels.AllKeys.Count;

            ExpansionPanels.AllKeys.Add(Key);

            return base.OnInitializedAsync();
        }
    }
}
