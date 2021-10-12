using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTab : BGroupItem<ItemGroupBase>
    {
        public BTab() : base(GroupType.SlideGroup)
        {
        }

        [CascadingParameter]
        public BTabs Tabs { get; set; }

        [CascadingParameter(Name = "DISPLAY:NONE")]
        public bool IsDisplayNone { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void OnInitialized()
        {
            if (!Matched || !IsDisplayNone) return;

            if (Value == null)
                Value = Tabs.Tabs.Count;

            if (Tabs.Tabs.Any(tab => tab.Value?.ToString() == Value?.ToString())) return;

            Tabs.AddTab(this);
        }

        public async Task HandleClick(MouseEventArgs args)
        {
            await OnClick.InvokeAsync(args);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (IsDisplayNone) return;

            if (firstRender)
            {
                await Tabs.CallSlider();
            }
        }
    }
}