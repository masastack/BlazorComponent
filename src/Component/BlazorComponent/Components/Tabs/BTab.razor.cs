using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTab : BGroupItem<ItemGroupBase>, IRoutable
    {
        public BTab() : base(GroupType.SlideGroup)
        {
        }

        [CascadingParameter]
        public BTabs Tabs { get; set; }

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public string Target { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Tabs.CallSlider();
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            IRoutable router = new Router(this);
            (Tag, Attributes) = router.GenerateRouteLink();
        }

        protected override bool AfterHandleEventShouldRender() => false;

        private async Task HandleOnClick(MouseEventArgs args)
        {
            await ToggleAsync();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}