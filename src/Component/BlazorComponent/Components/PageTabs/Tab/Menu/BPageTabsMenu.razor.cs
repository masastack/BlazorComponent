using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BPageTabsMenu<TTabs> : ComponentPartBase<TTabs>
        where TTabs : IPageTabs
    {
        EventCallback<MouseEventArgs> OnReload => CreateEventCallback<MouseEventArgs>(Component.HandleOnOnReloadAsync);

        EventCallback<MouseEventArgs> OnCloseLeft => CreateEventCallback<MouseEventArgs>(Component.HandleOnCloseLeftAsync);

        EventCallback<MouseEventArgs> OnCloseRight => CreateEventCallback<MouseEventArgs>(Component.HandleOnCloseRightAsync);

        EventCallback<MouseEventArgs> OnCloseOther => CreateEventCallback<MouseEventArgs>(Component.HandleOnCloseOtherAsync);
    }
}
