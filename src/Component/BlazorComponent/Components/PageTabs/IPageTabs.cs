using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IPageTabs : ITabs
    {
        IList<PageTabItem> ComputedItems { get; }
        string CloseIcon { get; }
        bool NoActiveItem { get; }
        RenderFragment<PageTabContentContext> TabContent { get; }
        bool IsActive(PageTabItem item);
        Task HandleOnOnReloadAsync(MouseEventArgs args);
        Task HandleOnCloseLeftAsync(MouseEventArgs arg);
        Task HandleOnCloseRightAsync(MouseEventArgs arg);
        Task HandleOnCloseOtherAsync(MouseEventArgs arg);
    }
}
