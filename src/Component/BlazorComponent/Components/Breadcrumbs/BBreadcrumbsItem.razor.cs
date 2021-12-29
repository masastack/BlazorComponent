using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BBreadcrumbsItem : BDomComponentBase, IBreadcrumbsItem, IBreadcrumbsDivider, IRoutable, ILinkable
    {
        private Linker _linker;
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
            _linker = new Linker(this);

            Breadcrumbs?.AddSubBreadcrumbsItem(this);

            NavigationManager.LocationChanged += OnLocationChanged;

            UpdateActiveForLinkage();
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            UpdateActiveForLinkage();
            InvokeStateHasChanged();
        }

        protected override void OnParametersSet()
        {
            _linker = new Linker(this);

            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();
        }

        #region When using razor definition without `Items` parameter

        protected bool IsLast => Breadcrumbs == null || Breadcrumbs.SubBreadcrumbsItems.Last() == this;

        public string Divider => Breadcrumbs?.Divider ?? "/";

        public RenderFragment DividerContent => Breadcrumbs?.DividerContent;

        #endregion

        private void UpdateActiveForLinkage()
        {
            if (IsLinkage)
            {
                Matched = _linker.MatchRoute(Href);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}