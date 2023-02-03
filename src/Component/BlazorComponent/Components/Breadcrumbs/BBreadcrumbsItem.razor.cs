using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorComponent
{
    public partial class BBreadcrumbsItem : BDomComponentBase, IBreadcrumbsItem, IBreadcrumbsDivider, IRoutable
    {
        private IRoutable _router;

        protected string WrappedTag { get; set; } = "li";

        protected bool Matched { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public BBreadcrumbs Breadcrumbs { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Exact { get; set; }

        [Parameter]
        public string? Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string? Tag { get; set; } = "div";

        [Parameter]
        public string? Target { get; set; }

        [Parameter]
        public string? Text { get; set; }

        [Parameter]
        public RenderFragment<(bool IsLast, bool IsDisabled)> ChildContent { get; set; }

        protected bool IsDisabled => Disabled || Matched;

        public bool IsRoutable => Href != null && (Breadcrumbs?.Routable is true);

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Breadcrumbs?.AddSubBreadcrumbsItem(this);

            NavigationManager.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                UpdateActiveForRoutable();
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var shouldRender = UpdateActiveForRoutable();
            if (shouldRender)
            {
                InvokeStateHasChanged();
            }
        }

        protected override void OnParametersSet()
        {
            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();
        }

        #region When using razor definition without `Items` parameter

        protected bool IsLast => Breadcrumbs == null || Breadcrumbs.SubBreadcrumbsItems.Last() == this;

        public string Divider => Breadcrumbs?.Divider ?? "/";

        public RenderFragment DividerContent => Breadcrumbs?.DividerContent;

        internal void InternalStateHasChanged()
        {
            StateHasChanged();
        }

        #endregion

        private bool UpdateActiveForRoutable()
        {
            var matched = Matched;

            if (IsRoutable)
            {
                Matched = _router.MatchRoute();
            }

            return matched != Matched;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
