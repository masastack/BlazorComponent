namespace BlazorComponent
{
    public partial class BDataFooterIcons<TComponent> where TComponent : IDataFooter
    {
        public bool ShowCurrentPage => Component.ShowCurrentPage;

        public DataOptions? Options => Component.Options;

        public EventCallback<MouseEventArgs> HandleOnPreviousPageAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnPreviousPageAsync);

        public EventCallback<MouseEventArgs> HandleOnNextPageAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnNextPageAsync);

        public EventCallback<MouseEventArgs> HandleOnFirstPageAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnFirstPageAsync);

        public EventCallback<MouseEventArgs> HandleOnLastPageAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnLastPageAsync);

        public bool RTL => Component.RTL;

        public string? NextIcon => Component.NextIcon;

        public string? PrevIcon => Component.PrevIcon;

        public string? LastIcon => Component.LastIcon;

        public string? FirstIcon => Component.FirstIcon;

        public bool DisableNextPageIcon => Component.DisableNextPageIcon;

        public bool ShowFirstLastPage => Component.ShowFirstLastPage;

        public DataPagination? Pagination => Component.Pagination;
    }
}
