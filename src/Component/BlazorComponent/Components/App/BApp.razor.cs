namespace BlazorComponent
{
    public abstract partial class BApp : BDomComponentBase, IDefaultsProvider
    {
        [Inject]
        private IPopupProvider PopupProvider { get; set; } = null!;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Not required that MasaBlazor service have to be ready first.
        /// Because the MasaBlazor service needs a JS interop to get the breakpoint and get ready to render child content.
        /// But in some cases, the JS interop cannot be called.
        /// For example, with CORS policy, the JS interop cannot be called when not signed in.
        /// </summary>
        /// <remarks>
        /// Not recommended to use this parameter.
        /// </remarks>
        [Parameter]
        public bool RenderFirst { get; set; }

        protected bool IsMasaBlazorReady { get; set; }

        protected virtual bool IsDark => false;

        public virtual IDictionary<string, IDictionary<string, object?>?>? Defaults { get; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            PopupProvider.StateChanged += OnStateChanged;
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
