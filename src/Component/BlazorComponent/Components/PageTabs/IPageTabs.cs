using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface IPageTabs : ITabs
    {
        IList<PageTabItem> ComputedItems { get; }
        string CloseIcon { get; }
        bool NoActiveItem { get; }
        RenderFragment<PageTabContentContext> TabContent { get; }
        bool IsActive(PageTabItem item);
        void Close(PageTabItem item);
        Task HandleOnOnReloadAsync(MouseEventArgs args);
        Task HandleOnCloseLeftAsync(MouseEventArgs arg);
        Task HandleOnCloseRightAsync(MouseEventArgs arg);
        Task HandleOnCloseOtherAsync(MouseEventArgs arg);
        string ReloadTabText { get; }
        string CloseTabsToTheLeftText { get; }
        string CloseTabsToTheRightText { get; }
        string CloseOtherTabsText { get; }
    }
}
