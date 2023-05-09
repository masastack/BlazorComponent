namespace BlazorComponent
{
    public interface ISelectList<TItem, TItemValue, TValue> : IHasProviderComponent
    {
        bool Action { get; }
        bool HideSelected { get; }
        IList<TItem> Items { get; }
        string? NoDataText { get; }
        RenderFragment<SelectListItemProps<TItem>>? ItemContent { get; }
        string GetFilteredText(TItem item);
        bool HasItem(TItem item);
        bool GetDisabled(TItem item);
        EventCallback<TItem> OnSelect { get; }
    }
}
