using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BChip : BGroupItem<ItemGroupBase>, IRoutable
    {
        protected IRoutable _router;

        public BChip() : base(GroupType.ChipGroup)
        {
        }

        [Parameter]
        public bool Active { get; set; } = true;

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string CloseIcon { get; set; }

        [Parameter]
        public string CloseLabel { get; set; } = "Close";

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "span";

        [Parameter]
        public string Target { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();
        }

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            if (Matched)
            {
                _ = (ItemGroup as BSlideGroup).SetWidths();
            }

            await ToggleItem();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}