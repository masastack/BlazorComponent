using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDataTable<TItem> : IDataIterator<TItem>
    {
        string Caption { get; }

        RenderFragment CaptionContent { get; }

        IEnumerable<DataTableHeader<TItem>> ComputedHeaders { get; }

        RenderFragment TopContent { get; }

        RenderFragment FootContent { get; }

        bool HasTop { get; }

        bool IsExpanded(TItem item);

        bool HasBottom { get; }

        Dictionary<string, object> ColspanAttrs { get; }

        Task HandleOnRowClickAsync(MouseEventArgs args);

        RenderFragment BodyPrependContent { get; }

        Task HandleOnRowContextMenuAsync(MouseEventArgs arg);

        RenderFragment BodyAppendContent { get; }

        RenderFragment GroupContent { get; }

        Task HandleOnRowDbClickAsync(MouseEventArgs arg);

        RenderFragment<(IEnumerable<DataTableHeader<TItem>> Headers, TItem Item)> ExpandedItemContent { get; }

        Func<TItem, string> ItemKey { get; }

        bool HideDefaultHeader { get; }

        bool ShowExpand { get; }

        RenderFragment ItemDataTableExpandContent { get; }

        bool ShowSelect { get; }

        string ExpandIcon { get; }

        RenderFragment<ItemColProps<TItem>> ItemColContent { get; }

        RenderFragment ItemDataTableSelectContent { get; }

        RenderFragment GroupHeaderContent { get; }

        Dictionary<string, bool> OpenCache { get; }

        string GroupMinusIcon { get; }

        string GroupCloseIcon { get; }

        string GroupPlusIcon { get; }

        DataOptions Options { get; }
    }
}

