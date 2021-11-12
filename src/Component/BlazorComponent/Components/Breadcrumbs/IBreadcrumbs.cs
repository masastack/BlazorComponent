using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public interface IBreadcrumbs<TItem> : IHasProviderComponent where TItem : BreadcrumbItem
    {    
        string Divider { get; }

        IReadOnlyList<TItem> Items { get; }

        RenderFragment DividerContent { get; }

        RenderFragment ChildContent { get; }

        RenderFragment<TItem> ItemContent { get; }
    }
}