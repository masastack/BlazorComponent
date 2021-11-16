using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    /// <summary>
    /// The component that can be rendered with <see cref="BBreadcrumbsLinkItem"/> or <see cref="BBreadcrumbsPlainItem"/>.
    /// </summary>
    public abstract partial class BBreadcrumbsItem : BDomComponentBase, IBreadcrumbsItem, IBreadcrumbsDivider, IRoutable
    {
        private IRoutable _router;

        protected string WrappedTag { get; set; } = "li";

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            _router = new Router(this);

            (Tag, Attributes) = _router.GenerateRouteLink();
        }

        #region When using razor definition without `Items` parameter

        [CascadingParameter]
        public BBreadcrumbs Breadcrumbs { get; set; }

        protected bool IsLast => Breadcrumbs == null || Breadcrumbs.SubBreadcrumbsItems.Last() == this;

        public string Divider => Breadcrumbs?.Divider ?? "/";

        public RenderFragment DividerContent => Breadcrumbs?.DividerContent;

        protected override void OnInitialized()
        {
            Breadcrumbs?.AddSubBreadcrumbsItem(this);
        }

        #endregion
    }
}