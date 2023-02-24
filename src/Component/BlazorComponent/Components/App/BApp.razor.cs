namespace BlazorComponent
{
    public abstract partial class BApp : BDomComponentBase, IThemeable
    {
        [Inject]
        private IPopupProvider PopupProvider { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        protected string ThemeStyleMarkups { get; set; } = "";

        protected override void OnInitialized()
        {
            PopupProvider.StateChanged += OnStateChanged;

            base.OnInitialized();
        }

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


        private void OnStateChanged(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            PopupProvider.StateChanged -= OnStateChanged;
        }
    }
}
