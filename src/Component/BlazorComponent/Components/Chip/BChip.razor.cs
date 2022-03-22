using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BChip : BGroupItem<ItemGroupBase>, IRoutable, IHandleEvent
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

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        public bool IsClickable => _router.IsClickable || Matched;

        public bool IsLink => _router.IsLink;

        public int Tabindex => _router.Tabindex;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();
        }

        async Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem item, object? arg)
        {
            await item.InvokeAsync(arg);
        }

        protected async Task HandleOnClick(MouseEventArgs args)
        {
            await ToggleAsync();

            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }
    }
}