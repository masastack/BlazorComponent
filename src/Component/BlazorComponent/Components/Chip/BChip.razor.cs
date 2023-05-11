namespace BlazorComponent
{
    public partial class BChip : BGroupItem<ItemGroupBase>, IRoutable
    {
        private IRoutable? _router;

        public BChip() : base(GroupType.ChipGroup)
        {
        }

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        [ApiDefaultValue(true)]
        public bool Active { get; set; } = true;

        [Parameter]
        public bool Close { get; set; }

        [Parameter]
        public string? CloseIcon { get; set; }

        [Parameter]
        [ApiDefaultValue("Close")]
        public string? CloseLabel { get; set; } = "Close";

        [Parameter]
        public string? Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool OnClickStopPropagation { get; set; }

        [Parameter]
        public bool OnClickPreventDefault { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseClick { get; set; }

        [Parameter]
        [ApiDefaultValue("span")]
        public string? Tag { get; set; } = "span";

        [Parameter]
        public string? Target { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool Exact { get; }

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

        public bool IsClickable => _router?.IsClickable is true || Matched;

        public bool IsLink => _router?.IsLink is true;

        public int Tabindex => _router?.Tabindex ?? 0;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();
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