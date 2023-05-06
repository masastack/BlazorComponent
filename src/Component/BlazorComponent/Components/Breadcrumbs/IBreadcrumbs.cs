namespace BlazorComponent
{
    public interface IBreadcrumbs : IHasProviderComponent
    {
        bool RenderDivider { get; }

        IReadOnlyList<BreadcrumbItem> Items { get; }

        RenderFragment<BreadcrumbItem>? ItemContent { get; }

        RenderFragment? ChildContent { get; }
    }
}
