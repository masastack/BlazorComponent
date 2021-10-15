using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComponent
{
    public interface IDataIterator<TItem> : IHasProviderComponent, ILoadable
    {
        bool IsEmpty { get; }

        RenderFragment ChildContent { get; }

        RenderFragment<(int Index, TItem Item)> ItemContent { get; }

        List<TItem> ComputedItems { get; }

        bool HideDefaultFooter { get; }

        RenderFragment HeaderContent { get; }

        RenderFragment FooterContent { get; }

        IEnumerable<TItem> Items { get; }

        DataPagination Pagination { get; }

        RenderFragment LoadingContent { get; }

        string LoadingText { get; }

        RenderFragment NoDataContent { get; }

        string NoDataText { get; }

        RenderFragment NoResultsContent { get; }

        string NoResultsText { get; }

        IEnumerable<IGrouping<string, TItem>> GroupedItems { get; }
    }
}

