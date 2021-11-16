using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IBreadcrumbsDivider: IHasProviderComponent
    {
        string Divider { get; }

        RenderFragment DividerContent { get; }
    }
}