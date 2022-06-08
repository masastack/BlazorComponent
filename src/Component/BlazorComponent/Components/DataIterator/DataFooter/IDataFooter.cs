namespace BlazorComponent
{
    public interface IDataFooter : IDataFooterParameters, IHasProviderComponent
    {
        IEnumerable<DataItemsPerPageOption> ComputedDataItemsPerPageOptions { get; }

        RenderFragment<(int PageStart, int PageStop, int ItemsLength)> PageTextContent { get; }

        Task HandleOnPreviousPageAsync();

        Task HandleOnNextPageAsync();

        bool RTL { get; }

        Task HandleOnFirstPageAsync();

        bool DisableNextPageIcon { get; }

        Task HandleOnLastPageAsync();
    }
}
