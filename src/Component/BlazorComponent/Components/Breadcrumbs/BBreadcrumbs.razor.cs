namespace BlazorComponent
{
    public partial class BBreadcrumbs : BDomComponentBase, IBreadcrumbs, IBreadcrumbsDivider, IThemeable, IAncestorRoutable
    {
        protected string Tag { get; init; } = "ul";

        public bool RenderDivider { get; protected set; } = true;

        [Parameter, MassApiParameter("/")]
        public string? Divider { get; set; } = "/";

        [Parameter]
        public RenderFragment? DividerContent { get; set; }

        [Parameter]
        public bool Routable { get; set; }

        [Parameter]
        public IReadOnlyList<BreadcrumbItem> Items { get; set; } = new List<BreadcrumbItem>();

        [Parameter]
        public RenderFragment<BreadcrumbItem>? ItemContent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

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

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                return;
            }

            if (SubBreadcrumbsItems.Any())
            {
                SubBreadcrumbsItems.ToList().ForEach(sub => sub.InternalStateHasChanged());
            }
        }

        #region When using razor definition without Items parameter

        internal List<BBreadcrumbsItem> SubBreadcrumbsItems { get; } = new();

        internal void AddSubBreadcrumbsItem(BBreadcrumbsItem item)
        {
            if (!SubBreadcrumbsItems.Contains(item))
            {
                SubBreadcrumbsItems.Add(item);
            }
        }

        #endregion
    }
}
