using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
