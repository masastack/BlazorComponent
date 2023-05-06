namespace BlazorComponent
{
    public partial class BChip : BGroupItem<ItemGroupBase>, IRoutable
    {
        protected IRoutable Router;

        public BChip() : base(GroupType.ChipGroup)
        {
        }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }

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

        public bool Exact { get; set; }

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

        public bool IsClickable => Router.IsClickable || Matched;

        public bool IsLink => Router.IsLink;

        public int Tabindex => Router.Tabindex;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Router = new Router(this);

            (Tag, Attributes) = Router.GenerateRouteLink();
        }

        protected override bool AfterHandleEventShouldRender() => false;

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