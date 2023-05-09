namespace BlazorComponent;

public interface IDataFooterParameters
{
    string? ItemsPerPageText { get; }
    DataOptions Options { get; }
    DataPagination Pagination { get; }
    EventCallback<Action<DataOptions>> OnOptionsUpdate { get; }
    IEnumerable<OneOf<int, DataItemsPerPageOption>>? ItemsPerPageOptions { get; set; }
    string? PrevIcon { get; set; }
    string? NextIcon { get; set; }
    string? LastIcon { get; set; }
    string? FirstIcon { get; set; }
    string? ItemsPerPageAllText { get; }
    bool ShowFirstLastPage { get; set; }
    bool ShowCurrentPage { get; }
    bool DisablePagination { get; }
    bool DisableItemsPerPage { get; }
    string? PageText { get; }
}
