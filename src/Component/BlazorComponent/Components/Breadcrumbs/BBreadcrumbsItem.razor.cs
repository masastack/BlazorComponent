using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BBreadcrumbsItem : BDomComponentBase, IBreadcrumbsItem, IBreadcrumbsDivider, IRoutable, ILinkable
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
        public string Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public bool Linkage { get; set; }

        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool IsDisabled => Disabled || Matched;

        public bool IsLinkage => Href != null && (Breadcrumbs?.Linkage ?? Linkage);

        protected override void OnInitialized()
        {
            Breadcrumbs?.AddSubBreadcrumbsItem(this);

            if (IsLinkage)
            {
                Matched = MatchRoute(NavigationManager.Uri);
            }

            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!IsLinkage) return;

            Matched = MatchRoute(e.Location);
        }

        private bool MatchRoute(string path)
        {
            var relativePath = NavigationManager.ToBaseRelativePath(path);
            if (Href.StartsWith("/"))
            {
                Href = Href[1..];
            }

            return string.Equals(Href, relativePath, StringComparison.OrdinalIgnoreCase);
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

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}