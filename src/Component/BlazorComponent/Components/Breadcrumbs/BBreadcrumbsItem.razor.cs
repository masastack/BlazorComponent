using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    /// <summary>
    /// The component that can be rendered with <see cref="BBreadcrumbsLinkItem"/> or <see cref="BBreadcrumbsPlainItem"/>.
    /// </summary>
    public abstract partial class BBreadcrumbsItem : BDomComponentBase, IBreadcrumbsItem, IBreadcrumbsDivider
    {
        protected string Tag { get; init; } = "li";

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected bool IsLink => Href != null;

        protected override void OnParametersSet()
        {
            ChildContent ??= builder => builder.AddContent(0, Text);
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