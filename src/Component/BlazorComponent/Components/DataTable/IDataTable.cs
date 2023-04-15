using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

/// <summary>
/// TODO: many render fragments lack the type parameter.
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface IDataTable<TItem> : IDataIterator<TItem>
{
    string Caption { get; }

    RenderFragment CaptionContent { get; }

    IEnumerable<DataTableHeader<TItem>> ComputedHeaders { get; }

    RenderFragment TopContent { get; }

    RenderFragment FootContent { get; }

    bool HasTop { get; }

    bool IsExpanded(TItem item);

    bool IsSelected(TItem item);

    bool HasBottom { get; }

    Dictionary<string, object> ColspanAttrs { get; }

    Task HandleOnRowClickAsync(DataTableRowMouseEventArgs<TItem> args);

    RenderFragment BodyPrependContent { get; }

    Task HandleOnRowContextmenuAsync(DataTableRowMouseEventArgs<TItem> args);

    RenderFragment BodyAppendContent { get; }

    RenderFragment GroupContent { get; }

    Task HandleOnRowDbClickAsync(DataTableRowMouseEventArgs<TItem> args);

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

    bool IsMobile { get; }

    bool OnRowContextmenuPreventDefault { get; }
}